﻿namespace TornBattleSimulator.Shared.Thunderdome.Damage;

[Flags]
public enum DamageFlags
{
    HitArmour = 1 << 0,
    MissedArmour = 1 << 1
}