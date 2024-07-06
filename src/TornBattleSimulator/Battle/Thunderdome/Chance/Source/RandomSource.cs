namespace TornBattleSimulator.Battle.Thunderdome.Chance.Source;

public class RandomSource : IRandomSource
{
    public double Next() => Random.Shared.NextDouble();
}