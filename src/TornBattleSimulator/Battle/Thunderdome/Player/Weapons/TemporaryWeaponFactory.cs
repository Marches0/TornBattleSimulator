using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Options;

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

    private Dictionary<TemporaryWeaponType, TemporaryWeaponOption> _temporaryWeapons;

    public TemporaryWeaponFactory(List<TemporaryWeaponOption> temporaryWeapons)
    {
        _temporaryWeapons = temporaryWeapons
            .ToDictionary(t => t.Name);
    }

    public Weapon GetTemporaryWeapon(TemporaryWeaponType weaponType)
    {
        TemporaryWeaponOption weapon = _temporaryWeapons[weaponType];
        return new Weapon()
        {
            Accuracy = weapon.Accuracy,
            Damage = weapon.Damage,
            Modifiers = weapon.Modifiers
                .Select(m => new ModifierDescription()
                {
                    Percent = 100,
                    Type = m
                })
                .ToList(),
            RateOfFire = TemporaryRateOfFire,
            Ammo = TemporaryAmmo,
            TemporaryWeaponType = weaponType
        };
    }
}