using TornBattleSimulator.Core.Thunderdome.Chance.Source;

namespace TornBattleSimulator.UnitTests.Chance;

internal class FixedRandomSource : IRandomSource
{
    private readonly double _roll;
    private readonly int _rangeRoll;

    public FixedRandomSource(
        double roll,
        int rangeRoll)
    {
        _roll = roll;
        _rangeRoll = rangeRoll;
    }

    public double Next() => _roll;

    public int Next(int min, int max) => _rangeRoll;
}
