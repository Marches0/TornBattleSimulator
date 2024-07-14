using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class WeaponContextBuilder
{
    private Ammo? _ammo = null;
    private RateOfFire? _rateOfFire = null;

    public WeaponContextBuilder WithAmmo(int magazines, int magazineSize)
    {
        _ammo = new Ammo() { Magazines = magazines, MagazineSize = magazineSize };
        return this;
    }

    public WeaponContextBuilder WithRateOfFire(int min, int max)
    {
        _rateOfFire = new RateOfFire() { Min = min, Max = max };
        return this;
    }

    public WeaponContext Build()
    {
        return new WeaponContext(
            new Weapon()
            {
                Ammo = _ammo,
                RateOfFire = _rateOfFire,
                Accuracy = 10,
                Damage = 10,
                Modifiers = new List<ModifierDescription>()
            },
            WeaponType.Melee,
            new List<PotentialModifier>()
        );
    }
}