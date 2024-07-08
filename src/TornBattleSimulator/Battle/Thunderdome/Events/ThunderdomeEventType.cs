namespace TornBattleSimulator.Battle.Thunderdome.Events;

public enum ThunderdomeEventType
{
    AttackHit = 1,
    AttackMiss,
    Reload,
    Stunned,
    EffectBegin,
    EffectEnd,
    DamageOverTime,
    Heal,
    FightBegin,
    FightEnd
}