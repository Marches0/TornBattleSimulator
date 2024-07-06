using TornBattleSimulator.Battle.Thunderdome.Accuracy.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Accuracy;
public class AccuracyCalculator
{
    private readonly SpeedDexterityAccuracyModifier _speedDexterityAccuracyModifier;

    public AccuracyCalculator(
        SpeedDexterityAccuracyModifier speedDexterityAccuracyModifier)
    {
        _speedDexterityAccuracyModifier = speedDexterityAccuracyModifier;
    }

    public double GetAccuracy(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        var statChance = _speedDexterityAccuracyModifier.GetHitChance(active, other);


        return 0;
        // Chaining doesn't work as well (as used for damage)
        // since more calculations are linked, so we
        // explicitly line up the components.

    }
}