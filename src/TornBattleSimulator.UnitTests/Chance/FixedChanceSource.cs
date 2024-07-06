using TornBattleSimulator.Battle.Thunderdome.Chance;

namespace TornBattleSimulator.UnitTests.Chance;

public class FixedChanceSource : IChanceSource
{
    private readonly bool _succeeds;

    public FixedChanceSource(bool succeeds)
    {
        _succeeds = succeeds;
    }

    public T ChooseWeighted<T>(IList<OptionChance<T>> options)
    {
        throw new NotImplementedException();
    }

    public bool Succeeds(double probability) => _succeeds;
}