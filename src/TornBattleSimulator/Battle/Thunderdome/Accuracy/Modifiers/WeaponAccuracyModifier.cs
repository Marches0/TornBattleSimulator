using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Accuracy.Modifiers;

public class WeaponAccuracyModifier
{
    public double GetHitChance(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        double statAccuracy)
    {
        double weaponAccuracyComponent = ((weapon.Description.Accuracy / 100) - 0.5) / 0.5;

        double modifier = statAccuracy >= 0.5
            ? statAccuracy + weaponAccuracyComponent * (1 - statAccuracy)
            : statAccuracy + weaponAccuracyComponent * statAccuracy;

        return modifier;
    }
}