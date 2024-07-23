﻿namespace TornBattleSimulator.Shared.Thunderdome.Events;

public enum ThunderdomeEventType
{
    AttackHit = 1,
    AttackMiss,
    UsedTemporary,
    Reload,
    Stunned,
    EffectBegin,
    EffectEnd,
    DamageOverTime,
    Heal,
    FightBegin,
    FightEnd,
    ChargeWeapon
}