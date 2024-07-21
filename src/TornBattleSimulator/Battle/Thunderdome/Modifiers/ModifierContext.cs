using System.Collections.ObjectModel;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Extensions;
using TornBattleSimulator.Battle.Thunderdome.Events;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stacking;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

public class ModifierContext : ITickable
{
    public ReadOnlyCollection<IModifier> Active => new ReadOnlyCollection<IModifier> (_activeModifiers.Select(m => m.Modifier).ToList());

    private List<ActiveModifier> _activeModifiers = new();
    private readonly PlayerContext _self;

    public ModifierContext(PlayerContext self)
    {
        _self = self;
    }

    public bool AddModifier(
        IModifier modifier,
        DamageResult? damageResult)
    {
        return modifier switch
        {
            IDamageOverTimeModifier dotModifier => AddDamageOverTime(dotModifier, damageResult!),
            IStackableStatModifier stackableStatModifier => AddStackingStatModifier(stackableStatModifier),
            _ => AddRegularModifier(modifier)
        };
    }

    private bool AddDamageOverTime(
        IDamageOverTimeModifier dotModifier,
        DamageResult damageResult)
    {
        // Can only have one DoT at a time
        if (_activeModifiers.OfType<ActiveDamageOverTimeModifier>().Any())
        {
            return false;
        }

        _activeModifiers.Add(new ActiveDamageOverTimeModifier(
            dotModifier.CreateLifespan(),
            dotModifier,
            damageResult)
        );

        return true;
    }

    private bool AddStackingStatModifier(IStackableStatModifier stackableStatModifier)
    {
        ActiveModifier? existingModifier = _activeModifiers
            .Where(m => m.Modifier is StackableStatModifierContainer c && c.Modifier == stackableStatModifier)
            .FirstOrDefault();

        StackableStatModifierContainer container;

        if (existingModifier == null)
        {
            container = new(stackableStatModifier, _self);
            _activeModifiers.Add(new ActiveModifier(container, container));
        }
        else
        {
            container = (StackableStatModifierContainer)existingModifier.Modifier;
        }

        return container.AddStack();
    }

    private bool AddRegularModifier(
        IModifier modifier)
    {
        _activeModifiers.Add(new ActiveModifier(modifier.CreateLifespan(), modifier));
        return true;
    }

    public void TurnComplete(ThunderdomeContext context)
    {
        foreach (ActiveModifier modifier in _activeModifiers)
        {
            modifier.CurrentLifespan.TurnComplete(context);
        }

        ExpireModifiers(context);
    }

    public void OwnActionComplete(ThunderdomeContext context)
    {
        foreach (ActiveModifier modifier in _activeModifiers)
        {
            modifier.CurrentLifespan.OwnActionComplete(context);
        }

        ExpireModifiers(context);
    }

    public void OpponentActionComplete(ThunderdomeContext context)
    {
        foreach (ActiveModifier modifier in _activeModifiers)
        {
            modifier.CurrentLifespan.OpponentActionComplete(context);
        }

        foreach (ActiveDamageOverTimeModifier dotModifier in _activeModifiers.OfType<ActiveDamageOverTimeModifier>())
        {
            dotModifier.Tick(context, _self);
        }

        ExpireModifiers(context);
    }

    private void ExpireModifiers(ThunderdomeContext context)
    {
        var expiredEvents = _activeModifiers
            .Where(m => m.CurrentLifespan.Expired)
            .Select(m => context.CreateEvent(_self, ThunderdomeEventType.EffectEnd, new EffectEndEvent(m.Modifier.Effect)));

        context.Events.AddRange(expiredEvents);

        _activeModifiers = _activeModifiers
            .Where(m => !m.CurrentLifespan.Expired)
            .ToList();
    }
}