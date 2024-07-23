using TornBattleSimulator.Battle.Thunderdome.Accuracy.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Accuracy;

public class AccuracyCalculator : IAccuracyCalculator
{
    private readonly ISpeedDexterityAccuracyModifier _speedDexterityAccuracyModifier;
    private readonly IWeaponAccuracyModifier _weaponAccuracyModifier;

    public AccuracyCalculator(
        ISpeedDexterityAccuracyModifier speedDexterityAccuracyModifier,
        IWeaponAccuracyModifier weaponAccuracyModifier)
    {
        _speedDexterityAccuracyModifier = speedDexterityAccuracyModifier;
        _weaponAccuracyModifier = weaponAccuracyModifier;
    }

    public double GetAccuracy(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        // Chaining a single interface doesn't work as well (as used for damage)
        // since more calculations are linked, so we
        // explicitly line up the components.
        double statChance = _speedDexterityAccuracyModifier.GetHitChance(active, other);
        double totalChance = _weaponAccuracyModifier.GetHitChance(active, other, weapon, statChance);

        return totalChance;
    }
}