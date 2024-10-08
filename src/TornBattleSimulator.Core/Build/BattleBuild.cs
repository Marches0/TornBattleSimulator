﻿using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Strategy;

namespace TornBattleSimulator.Core.Build;

// Supported by validator + model builder
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class BattleBuild
{
    public string Name { get; set; }

    public int Health { get; set; }

    public int Level { get; set; }

    public BattleStats BattleStats { get; set; }

    // Make these nullable to support not having the weapon?
    public Weapon Primary { get; set; }

    public Weapon Secondary { get; set; }

    public Weapon Melee { get; set; }

    public TemporaryWeaponType? Temporary { get; set; }

    public ArmourSet? Armour { get; set; }

    public List<StrategyDescription> Strategy { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.