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
        if (active.Primary.Ammo.MagazineAmmoRemaining == 0)
        {
            throw new InvalidOperationException("Attempted to fire primary without ammo.");
        }

        int damage = _damageCalculator.CalculateDamage(context, active, other);
        other.Health -= damage;

        int ammoConsumed = Random.Shared.Next((int)active.Build.Primary.RateOfFire.Min, (int)active.Build.Primary.RateOfFire.Max + 1);
        active.Primary.Ammo!.MagazineAmmoRemaining = (uint)Math.Max(0, active.Primary.Ammo.MagazineAmmoRemaining - ammoConsumed);
    }
}