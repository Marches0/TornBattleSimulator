using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Accuracy;
public class AccuracyCalculator
{
    public double GetAccuracy(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        return 0;
        // Chaining doesn't work as well (as used for damage)
        // since more calculations are linked, so we
        // explicitly line up the components.

    }
}