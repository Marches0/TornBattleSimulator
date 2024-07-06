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
        double baseHitChance = statAccuracy * 100;
        double weaponAccuracy = weapon.Description.Accuracy * 100;

        var weaponAccComponent = (weaponAccuracy - 50d) / 50d;

        var modifier = baseHitChance >= 50
            ? baseHitChance + weaponAccComponent * (100 - baseHitChance)
            : baseHitChance + weaponAccComponent * baseHitChance;

        return modifier / 100;
    }
}