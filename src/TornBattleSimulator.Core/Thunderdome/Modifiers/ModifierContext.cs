using System.Collections.ObjectModel;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stackable;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Charge;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

/// <inheritdoc/>
public class ModifierContext : IModifierContext
{
    /// <inheritdoc/>
    public ReadOnlyCollection<IModifier> Active => new ReadOnlyCollection<IModifier>(
        _activeModifiers.Select(m => m.Modifier)
        .Concat(ChargeModifiers.Select(c => c.Modifier))
        .ToList()
    );

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public bool AddModifier(
        IModifier modifier,
        AttackResult? attackResult)
    {
        return modifier switch
        {
            IDamageOverTimeModifier dot => AddDamageOverTime(dot, attackResult!),
            IStackableStatModifier stackableStat => AddStackingStatModifier(stackableStat),
            IChargeableModifier chargeable => AddChargeable(chargeable),
            IExclusiveModifier exclusive => AddExclusive(exclusive),
            _ => AddRegularModifier(modifier)
        };
    }

    /// <inheritdoc/>
    public void AttackComplete(AttackContext attack)
    {
        // Should this be in ITickable? Would require
        // rearranging bits to include Attack in some events
        var customExpired = _activeModifiers
            .Where(am => am.Modifier.Lifespan.LifespanType == ModifierLifespanType.Custom)
            .Select(am => new
            {
                Lifespan = (CustomLifespan)am.CurrentLifespan,
                Modifier = (IOwnedLifespan)am.Modifier
            });
            
        foreach (var custom in customExpired)
        {
            custom.Lifespan.SetExpiry(attack, custom.Modifier);
        }
    }

    /// <inheritdoc/>
    public void FightBegin(ThunderdomeContext context)
    {
        foreach (ITickable tickable in _tickables)
        {
            tickable.FightBegin(context);
        }

        ExpireModifiers(context);
    }

    /// <inheritdoc/>
    public void TurnComplete(ThunderdomeContext context)
    {
        foreach (ITickable tickable in _tickables)
        {
            tickable.TurnComplete(context);
        }

        ExpireModifiers(context);
    }

    /// <inheritdoc/>
    public void OwnActionComplete(ThunderdomeContext context)
    {
        foreach (ITickable tickable in _tickables)
        {
            tickable.OwnActionComplete(context);
        }

        ExpireModifiers(context);
    }

    /// <inheritdoc/>
    public void OpponentActionComplete(ThunderdomeContext context)
    {
        foreach (ITickable tickable in _tickables)
        {
            tickable.OpponentActionComplete(context);
        }

        ExpireModifiers(context);
    }

    private bool AddExclusive(IExclusiveModifier exclusive)
    {
        return _activeModifiers.All(m => m.Modifier.Effect != exclusive.Effect)
            ? AddRegularModifier(exclusive)
            : false;
    }

    private bool AddDamageOverTime(
        IDamageOverTimeModifier dot,
        AttackResult attackResult)
    {
        if (attackResult.Damage == null)
        {
            throw new InvalidOperationException("Cannot apply DoT without damage");
        }

        // Can only have one DoT at a time
        if (_activeModifiers.OfType<ActiveDamageOverTimeModifier>().Any())
        {
            return false;
        }

        _activeModifiers.Add(new ActiveDamageOverTimeModifier(
            dot.CreateLifespan(),
            dot,
            _self,
            attackResult.Damage)
        );

        return true;
    }

    private bool AddStackingStatModifier(IStackableStatModifier stackableStat)
    {
        ActiveModifier? existingModifier = _activeModifiers
            .Where(m => m.Modifier is StackableStatModifierContainer c && c.Modifier == stackableStat)
            .FirstOrDefault();

        StackableStatModifierContainer container;

        if (existingModifier == null)
        {
            container = new(stackableStat, _self);
            _activeModifiers.Add(new ActiveModifier(container, container));
        }
        else
        {
            container = (StackableStatModifierContainer)existingModifier.Modifier;
        }

        return container.AddStack();
    }

    private bool AddChargeable(IChargeableModifier chargeable)
    {
        ChargeModifiers.Add(new ChargedModifierContainer(chargeable));
        return true;
    }

    private bool AddRegularModifier(
        IModifier modifier)
    {
        _activeModifiers.Add(new ActiveModifier(modifier.CreateLifespan(), modifier));
        return true;
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