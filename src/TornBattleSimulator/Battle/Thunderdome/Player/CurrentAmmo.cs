using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Player;

public class CurrentAmmo : Ammo
{
    public uint MagazineAmmoRemaining { get; set; }
    public uint MagazinesRemaining { get; set; }
}