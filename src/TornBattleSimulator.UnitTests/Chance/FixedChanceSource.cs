using TornBattleSimulator.Core.Thunderdome.Chance;

namespace TornBattleSimulator.UnitTests.Chance;

public class FixedChanceSource : IChanceSource
{
    private readonly bool _succeeds;
    private readonly int? _rangeRoll;
    public static readonly FixedChanceSource AlwaysSucceeds = new(true);
    public static readonly FixedChanceSource AlwaysFails = new(false);

    public FixedChanceSource(
        bool succeeds,
        int? rangeRoll = null)
    {
        _succeeds = succeeds;
        _rangeRoll = rangeRoll;
    }

    public T ChooseWeighted<T>(IList<OptionChance<T>> options)
    {
        throw new NotImplementedException();
    }

    public bool Succeeds(double probability) => _succeeds;

    public int ChooseRange(int min, int max)
    {
        return _rangeRoll ?? throw new ArgumentNullException("No rangeRoll provided.");
    }
}