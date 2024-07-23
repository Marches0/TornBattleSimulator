using TornBattleSimulator.Battle.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Accuracy.Modifiers;

public class WeaponAccuracyModifier : IWeaponAccuracyModifier
{
    public double GetHitChance(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        double statAccuracy)
    {
        double weaponAccuracy = (weapon.Description.Accuracy / 100);
        weaponAccuracy -= (active.Modifiers.Active.OfType<BlindfireModifier>().Count() * 0.05);

        double weaponAccuracyComponent = (weaponAccuracy - 0.5) / 0.5;

        double modifier = statAccuracy >= 0.5
            ? statAccuracy + weaponAccuracyComponent * (1 - statAccuracy)
            : statAccuracy + weaponAccuracyComponent * statAccuracy;

        return modifier;
    }
}

public interface IWeaponAccuracyModifier
{
    double GetHitChance(PlayerContext active, PlayerContext other, WeaponContext weapon, double statAccuracy);
}