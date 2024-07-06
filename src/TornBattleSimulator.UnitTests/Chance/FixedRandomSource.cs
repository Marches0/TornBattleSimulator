using TornBattleSimulator.Battle.Thunderdome.Chance.Source;

namespace TornBattleSimulator.UnitTests.Chance;

internal class FixedRandomSource : IRandomSource
{
    private readonly double _roll;

    public FixedRandomSource(double roll)
    {
        _roll = roll;
    }

    public double Next() => _roll;
}
