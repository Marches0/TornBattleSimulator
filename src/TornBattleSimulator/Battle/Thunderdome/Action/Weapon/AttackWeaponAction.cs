using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public abstract class AttackWeaponAction 
{
    private readonly IDamageCalculator _damageCalculator;

    protected AttackWeaponAction(
        IDamageCalculator damageCalculator)
    {
        _damageCalculator = damageCalculator;
    }

    protected void PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        if (weapon.Ammo is { MagazineAmmoRemaining: 0 })
        {
            throw new InvalidOperationException("Cannot use loaded weapon without ammo.");
        }

        int damage = _damageCalculator.CalculateDamage(context, active, other);
        other.Health -= damage;

        if (weapon.Ammo != null)
        {
            int ammoConsumed = Random.Shared.Next(weapon.Weapon.RateOfFire.Min, weapon.Weapon.RateOfFire.Max + 1);
            weapon.Ammo!.MagazineAmmoRemaining = Math.Max(0, weapon.Ammo.MagazineAmmoRemaining - ammoConsumed);
        }
    }
}