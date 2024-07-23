using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Damage;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Shared.Extensions;
using TornBattleSimulator.Shared.Thunderdome.Events.Data;

namespace TornBattleSimulator.Shared.Thunderdome.Modifiers.DamageOverTime;

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

    public void TurnComplete(ThunderdomeContext context)
    {
        _primed = true;
    }

    public void OwnActionComplete(ThunderdomeContext context) { }

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
}