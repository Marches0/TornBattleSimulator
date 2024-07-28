using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Events.Data;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.DamageOverTime;

/// <summary>
///  A tracked Damage over Time modifier.
/// </summary>
public class ActiveDamageOverTimeModifier : ActiveModifier, ITickable
{
    private readonly int _appliedDamage;
    private bool _primed = false;
    private int _turnsActive = 0;
    private readonly IDamageOverTimeModifier _damageOverTimeModifier;
    private readonly PlayerContext _target;

    public ActiveDamageOverTimeModifier(
        IModifierLifespan currentLifespan,
        IDamageOverTimeModifier modifier,
        PlayerContext target,
        DamageResult damageContext) : base(currentLifespan, modifier)
    {
        _damageOverTimeModifier = modifier;
        _target = target;
        _appliedDamage = damageContext.Damage;
    }

    /// <inheritdoc/>
    public void TurnComplete(ThunderdomeContext context)
    {
        _primed = true;
    }

    /// <inheritdoc/>
    public void OwnActionComplete(ThunderdomeContext context) { }

    /// <inheritdoc/>
    public void OpponentActionComplete(ThunderdomeContext context)
    {
        // After the person who applied the DoT (the opponent) takes action, it ticks.
        // But only *after* the turn when it was applied is complete - it does
        // not tick on the same turn it was applied.
        if (_primed)
        {
            ++_turnsActive;
            var damage = (int)(_appliedDamage * Math.Pow(_damageOverTimeModifier.Decay, _turnsActive));

            _target.Health.CurrentHealth -= damage;

            var dotEvent = context.CreateEvent(_target, ThunderdomeEventType.DamageOverTime, new DamageOverTimeEvent(damage, _damageOverTimeModifier.Effect));
            context.Events.Add(dotEvent);
        }
    }

    /// <inheritdoc/>
    public void FightBegin(ThunderdomeContext context){ }
}