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
    private WeaponType _weaponType = WeaponType.Primary;

    public WeaponContextBuilder OfType(WeaponType weaponType)
    {
        _weaponType = weaponType;
        return this;
    }

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
        if (modifier.ValueBehaviour == ModifierValueBehaviour.Chance)
        {
            _modifiers.Add(modifier);
        }
        else
        {
            _autoModifiers.Add(modifier);
        }
        
        return this;
    }

    public WeaponContextBuilder WithModifiers(IEnumerable<IModifier> modifiers)
    {
        foreach (var modifier in modifiers)
        {
            WithModifier(modifier);
        }

        return this;
    }

    public WeaponContext Build()
    {
        var context = new WeaponContext(
            new Weapon()
            {
                Ammo = _ammo,
                RateOfFire = _rateOfFire,
                Accuracy = _accuracy,
                Damage = 10,
                Modifiers = new List<ModifierDescription>()
            },
            _weaponType,
            _modifiers.Select(m => new PotentialModifier(m, 1)).ToList()
        );

        context.Modifiers = new ModifierContext(null);
        
        // just for testing I guess. use a special class for auto ones?
        foreach (var auto in _autoModifiers)
        {
            context.Modifiers.AddModifier(auto, null);
        }

        return context;
    }
}