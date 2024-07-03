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

    protected ThunderdomeEvent PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        // Should we split into seperate actions for loaded and unloaded?
        if (weapon.Ammo != null && weapon.Ammo.MagazineAmmoRemaining == 0)
        {
            throw new InvalidOperationException("Cannot use loaded weapon without ammo.");
        }

        int damage = _damageCalculator.CalculateDamage(context, active, other);
        other.Health -= damage;

        if (weapon.Ammo != null)
        {
            int ammoConsumed = Random.Shared.Next(weapon.Description.RateOfFire.Min, weapon.Description.RateOfFire.Max + 1);
            weapon.Ammo!.MagazineAmmoRemaining = Math.Max(0, weapon.Ammo.MagazineAmmoRemaining - ammoConsumed);
        }

        return new ThunderdomeEvent(
            active.PlayerType,
            ThunderdomeEventType.AttackHit,
            context.Turn,
            [damage]
        );
    }
}