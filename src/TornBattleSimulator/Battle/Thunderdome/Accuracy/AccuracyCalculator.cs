using TornBattleSimulator.Battle.Thunderdome.Accuracy.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Accuracy;

public class AccuracyCalculator
{
    private readonly SpeedDexterityAccuracyModifier _speedDexterityAccuracyModifier;
    private readonly WeaponAccuracyModifier _weaponAccuracyModifier;

    public AccuracyCalculator(
        SpeedDexterityAccuracyModifier speedDexterityAccuracyModifier,
        WeaponAccuracyModifier weaponAccuracyModifier)
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