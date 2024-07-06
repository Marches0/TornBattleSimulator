namespace TornBattleSimulator.Battle.Thunderdome.Chance;

public interface IChanceSource
{
    bool Succeeds(double probability);
    T ChooseWeighted<T>(IList<OptionChance<T>> options);
}
