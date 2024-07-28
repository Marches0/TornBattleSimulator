using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Ammo;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

public class AmmoCalculator : IAmmoCalculator
{
    private readonly IChanceSource _chanceSource;

    public AmmoCalculator(
        IChanceSource chanceSource)
    {
        _chanceSource = chanceSource;
    }

    public int GetAmmoRemaining(
        PlayerContext active,
        WeaponContext weapon)
    {
        double baseConsumed = _chanceSource.ChooseRange(weapon.Description.RateOfFire.Min, weapon.Description.RateOfFire.Max + 1);
        int ammoConsumed = (int)weapon.Modifiers.Active
            .OfType<IAmmoModifier>()
            .Aggregate(baseConsumed, (total, modifier) => total * modifier.GetModifier());

        int remainingAmmo = weapon.Ammo!.MagazineAmmoRemaining - ammoConsumed;

        return Math.Max(0, remainingAmmo);
    }
}