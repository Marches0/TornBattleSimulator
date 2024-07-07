namespace TornBattleSimulator.Battle.Thunderdome.Accuracy.Modifiers;

public class SpeedDexterityAccuracyModifier : ISpeedDexterityAccuracyModifier
{
    private const double fiftyDivSeven = 50d / 7;

    public double GetHitChance(PlayerContext active, PlayerContext other)
    {
        // https://www.torn.com/forums.php#/p=threads&f=61&t=16199413&b=0&a=0
        double ratio = (double)active.Stats.Speed / (double)other.Stats.Dexterity;
        double mod = ratio <= 1
            ? fiftyDivSeven
                * (8 * Math.Sqrt(ratio) - 1)
            : 100 - fiftyDivSeven
                * (8 * Math.Sqrt(1 / ratio) - 1);

        return Math.Clamp(mod / 100, 0, 1);
    }
}

public interface ISpeedDexterityAccuracyModifier
{
    double GetHitChance(PlayerContext active, PlayerContext other);
}