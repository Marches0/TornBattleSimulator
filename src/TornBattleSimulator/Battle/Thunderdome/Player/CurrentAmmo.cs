﻿using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Player;

public class CurrentAmmo : Ammo
{
    public int MagazineAmmoRemaining { get; set; }
    public int MagazinesRemaining { get; set; }
}