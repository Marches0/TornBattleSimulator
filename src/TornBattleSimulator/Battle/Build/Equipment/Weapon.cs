namespace TornBattleSimulator.Battle.Build.Equipment;

public class Weapon
{
    public double Damage { get; set; }

    public double Accuracy { get; set; }

    public RateOfFire RateOfFire { get; set; }

    public Ammo Ammo { get; set; }

    public List<ModifierDescription> Modifiers { get; set; }
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