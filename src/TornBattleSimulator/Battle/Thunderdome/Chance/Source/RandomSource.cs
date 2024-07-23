using TornBattleSimulator.Shared.Thunderdome.Chance.Source;

namespace TornBattleSimulator.Battle.Thunderdome.Chance.Source;

public class RandomSource : IRandomSource
{
    public double Next() => Random.Shared.NextDouble() + double.Epsilon; // Don't want to get a 0 back.

    public int Next(int min, int max) => Random.Shared.Next(min, max);
}