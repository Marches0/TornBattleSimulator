namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application.Chance.Source;

public class RandomSource : IRandomSource
{
    public double Next() => Random.Shared.NextDouble();
}