using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Extensions;
using TornBattleSimulator.Battle.Thunderdome.Events;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.DamageOverTime;

public class ActiveDamageOverTimeModifier : ActiveModifier
{
    private readonly int _appliedDamage;
    private int _turnsActive = 0;
    private readonly IDamageOverTimeModifier _damageOverTimeModifier;

    public ActiveDamageOverTimeModifier(
        IModifierLifespan currentLifespan,
        IDamageOverTimeModifier modifier,
        DamageResult damageContext) : base(currentLifespan, modifier)
    {
        _damageOverTimeModifier = modifier;
        _appliedDamage = damageContext.Damage;
    }

    // implement ITickable?
    public void Tick(
        ThunderdomeContext context,
        PlayerContext target)
    {
        ++_turnsActive;
        var damage = (int)(_appliedDamage * Math.Pow(_damageOverTimeModifier.Decay, _turnsActive));

        target.Health.CurrentHealth -= damage;

        var dotEvent = context.CreateEvent(target, ThunderdomeEventType.DamageOverTime, new DamageOverTimeEvent(damage, _damageOverTimeModifier.Effect));
        context.Events.Add(dotEvent);
    }
}