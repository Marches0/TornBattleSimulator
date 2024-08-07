namespace TornBattleSimulator.Core.Thunderdome.Events;

public enum ThunderdomeEventType
{
    AttackHit = 1,
    AttackMiss,
    UsedTemporary,
    Reload,
    MissedTurn,
    EffectBegin,
    EffectEnd,
    DamageOverTime,
    Heal,
    FightBegin,
    FightEnd,
    ChargeWeapon,
    ExtraDamage,
    Disarmed
}