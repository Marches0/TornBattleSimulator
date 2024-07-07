using System.Collections.ObjectModel;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Extensions;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

public class ModifierContext
{
    public ReadOnlyCollection<IModifier> Active => new ReadOnlyCollection<IModifier> (_activeModifiers.Select(m => m.Modifier).ToList());

    private List<ActiveModifier> _activeModifiers = new();

    public void Tick(
        ThunderdomeContext context,
        PlayerContext self)
    {
        foreach (ActiveModifier modifier in _activeModifiers)
        {
            modifier.CurrentLifespan.Tick(context);
        }

        foreach (ActiveDamageOverTimeModifier dotModifier in _activeModifiers.OfType<ActiveDamageOverTimeModifier>())
        {
            dotModifier.Tick(context, self);
        }

        var expiredEvents = _activeModifiers
            .Where(m => m.CurrentLifespan.Expired)
            .Select(m => context.CreateEvent(self, ThunderdomeEventType.EffectEnd, new EffectEndEvent(m.Modifier.Effect)));

        context.Events.AddRange(expiredEvents);

        _activeModifiers = _activeModifiers
            .Where(m => !m.CurrentLifespan.Expired)
            .ToList();
    }

    public bool AddModifier(IModifier modifier, DamageResult? damageResult)
    {
        if (modifier is IDamageOverTimeModifier dotModifier)
        {
            // Can only have one DoT at a time
            if (_activeModifiers.OfType<ActiveDamageOverTimeModifier>().Any())
            {
                return false;
            }

            _activeModifiers.Add(new ActiveDamageOverTimeModifier(
                CreateLifespan(dotModifier),
                dotModifier,
                damageResult!)
            );

            return true;
        }

        _activeModifiers.Add(new ActiveModifier(CreateLifespan(modifier), modifier));
        return true;
    }

    private IModifierLifespan CreateLifespan(IModifier modifier)
    {
        return modifier.Lifespan.LifespanType switch
        {
            ModifierLifespanType.Temporal => new TemporalModifierLifespan(modifier.Lifespan.Duration!.Value),
            ModifierLifespanType.Turns => new TurnModifierLifespan(modifier.Lifespan.TurnCount!.Value),
            _ => throw new NotImplementedException($"{modifier.Lifespan.LifespanType} not supported.")
        };
    }
}