namespace TornBattleSimulator.Extensions;

public static class NumberExtensions
{
    public static string ToSimpleString(this ulong value)
    {
        if (value < 10000)
        {
            return value.ToString();
        }

        if (value < 1_000_000)
        {
            ulong thousands = value / 1000;
            return $"{thousands}k";
        }

        if (value < 1_000_000_000)
        {
            double millions = (double)value / 1_000_000;
            return $"{millions:F2}m";
        }

        if (value < 1_000_000_000_000)
        {
            double billions = (double)value / 1_000_000_000;
            return $"{billions:F2}b";
        }

        double trillions = (double)value / 1_000_000_000_000;
        return $"{trillions:F2}t";
    }
}