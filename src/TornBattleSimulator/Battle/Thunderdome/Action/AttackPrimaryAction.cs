using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.Battle.Thunderdome.Action;

public class AttackPrimaryAction : IAction
{
    private readonly IDamageCalculator _damageCalculator;

    public AttackPrimaryAction(
        IDamageCalculator damageCalculator)
    {
        _damageCalculator = damageCalculator;
    }

    public void PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other)
    {
        if (active.PrimaryAmmo.MagazineAmmoRemaining == 0)
        {
            throw new InvalidOperationException("Attempted to fire primary without ammo.");
        }

        int damage = (int)_damageCalculator.CalculateDamage(context, active, other);
        other.Health -= damage;

        int ammoConsumed = Random.Shared.Next((int)active.Build.Primary.RateOfFire.Min, (int)active.Build.Primary.RateOfFire.Max + 1);
        active.PrimaryAmmo!.MagazineAmmoRemaining = (uint)Math.Max(0, active.PrimaryAmmo.MagazineAmmoRemaining - ammoConsumed);
    }
}