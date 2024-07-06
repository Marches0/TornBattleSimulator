using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

public class TemporaryWeaponFactory
{
    private static readonly Ammo TemporaryAmmo = new Ammo()
    {
        Magazines = 0,
        MagazineSize = 1
    };

    private static readonly RateOfFire TemporaryRateOfFire = new RateOfFire()
    {
        Min = 1,
        Max = 1
    };

    // put these in json like armour
    public Weapon GetTemporaryWeapon(TemporaryWeaponType weaponType)
    {
        return weaponType switch
        {
            TemporaryWeaponType.TearGas => _tearGas.Value,
            _ => throw new NotImplementedException(weaponType.ToString())
        };
    }

    private Lazy<Weapon> _tearGas = new Lazy<Weapon>(() => new Weapon() 
    {
        Accuracy = 200,
        Damage = 0,
        Ammo = TemporaryAmmo,
        RateOfFire = TemporaryRateOfFire,
        Modifiers = new List<ModifierDescription>()
        {
            new()
            {
                Type = ModifierType.Gassed,
                Percent = 100
            }
        }
    });
}