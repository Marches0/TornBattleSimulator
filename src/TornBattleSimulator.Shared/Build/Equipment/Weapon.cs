using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Shared.Build.Equipment;

public class Weapon
{
    public double Damage { get; set; }

    // Should really make this [0, 1]
    public double Accuracy { get; set; }

    public RateOfFire RateOfFire { get; set; }

    public Ammo Ammo { get; set; }

    public List<ModifierDescription> Modifiers { get; set; }

    // meh. just stuck it on here.
    public TemporaryWeaponType? TemporaryWeaponType { get; set; }
}

public class RateOfFire
{
    public int Min { get; set; }
    public int Max { get; set; }
}

public class Ammo
{
    public int Magazines { get; set; }
    public int MagazineSize { get; set; }
}