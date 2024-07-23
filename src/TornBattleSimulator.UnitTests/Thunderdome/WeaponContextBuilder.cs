using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class WeaponContextBuilder
{
    private Ammo? _ammo = null;
    private RateOfFire? _rateOfFire = null;
    private List<IModifier> _modifiers = new List<IModifier>();
    private List<IModifier> _autoModifiers = new List<IModifier>();
    private double _accuracy = 10;
    private double _damage = 10;

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

    public WeaponContextBuilder WithAccuracy(double accuracy)
    {
        _accuracy = accuracy;
        return this;
    }

    public WeaponContextBuilder WithDamage(double damage)
    {
        _damage = damage;
        return this;
    }

    public WeaponContextBuilder WithModifier(IModifier modifier)
    {
        if (modifier is IAutoActivateModifier)
        {
            _autoModifiers.Add(modifier);
        }
        else
        {
            _modifiers.Add(modifier);
        }
        
        return this;
    }

    public WeaponContext Build()
    {
        return new WeaponContext(
            new Weapon()
            {
                Ammo = _ammo,
                RateOfFire = _rateOfFire,
                Accuracy = _accuracy,
                Damage = 10,
                Modifiers = new List<ModifierDescription>()
            },
            WeaponType.Melee,
            _modifiers.Select(m => new PotentialModifier(m, 1)).ToList(),
            _autoModifiers
        );
    }
}