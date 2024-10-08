﻿using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Options;
public class TemporaryWeaponOption
{
    public TemporaryWeaponType Name { get; set; }
    public double Damage { get; set; }
    public double Accuracy { get; set; }
    public List<ModifierType> Modifiers { get; set; } = new();
}