using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TornBattleSimulator.Input.Build.Gear;

public class WeaponInput
{
    public double? Damage { get; set; }
    public double? Accuracy { get; set; }
    public RateOfFireInput? RateOfFire { get; set; }
    public AmmoInput? Ammo { get; set; }
}

public class RateOfFireInput
{
    public uint? Min { get; set; }
    public uint? Max { get; set; }
}

public class AmmoInput
{
    public uint? Magazines { get; set; }
    public uint? MagazineSize { get; set; }
}