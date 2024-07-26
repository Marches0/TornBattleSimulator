using System.Collections.ObjectModel;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stackable;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Charge;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

public class ModifierContext : ITickable
{
    public ReadOnlyCollection<IModifier> Active => new ReadOnlyCollection<IModifier>(
        _activeModifiers.Select(m => m.Modifier)
        .Concat(ChargeModifiers.Select(c => c.Modifier))
        .ToList()
    );

    public List<ChargedModifierContainer> ChargeModifiers { get; } = new();

    private List<ActiveModifier> _activeModifiers = new();

    // add charge?
    private IEnumerable<ITickable> _tickables =>
        _activeModifiers.Select(m => m.CurrentLifespan)
        .Concat<ITickable>(_activeModifiers.OfType<ActiveDamageOverTimeModifier>());

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
            IChargeableModifier chargeableModifier => AddChargeable(chargeableModifier),
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
            _self,
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

    private bool AddChargeable(IChargeableModifier chargeableModifier)
    {
        ChargeModifiers.Add(new ChargedModifierContainer(chargeableModifier));
        return true;
    }

    private bool AddRegularModifier(
        IModifier modifier)
    {
        _activeModifiers.Add(new ActiveModifier(modifier.CreateLifespan(), modifier));
        return true;
    }

    public void TurnComplete(ThunderdomeContext context)
    {
        foreach (ITickable tickable in _tickables)
        {
            tickable.TurnComplete(context);
        }

        ExpireModifiers(context);
    }

    public void OwnActionComplete(ThunderdomeContext context)
    {
        foreach (ITickable tickable in _tickables)
        {
            tickable.OwnActionComplete(context);
        }

        ExpireModifiers(context);
    }

    public void OpponentActionComplete(ThunderdomeContext context)
    {
        foreach (ITickable tickable in _tickables)
        {
            tickable.OpponentActionComplete(context);
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