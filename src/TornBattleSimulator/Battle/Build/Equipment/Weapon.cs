namespace TornBattleSimulator.Battle.Build.Equipment;

public class Weapon
{
    public double Damage { get; set; }

    public double Accuracy { get; set; }

    public RateOfFire RateOfFire { get; set; }

    public Ammo Ammo { get; set; }
}

public class RateOfFire
{
    public uint Min { get; set; }
    public uint Max { get; set; }
}

public class Ammo
{
    public uint Magazines { get; set; }
    public uint MagazineSize { get; set; }
}