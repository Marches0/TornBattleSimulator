﻿using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Description;

public class StrategyDescription
{
    public WeaponType Weapon { get; set; }

    public bool Reload { get; set; }

    public List<StrategyUntil> Until { get; set; }
}