namespace TornBattleSimulator.Battle.Thunderdome.Chance;

public interface IChanceSource
{
    bool Succeeds(double probability);

    int ChooseRange(int min, int max);

    T ChooseWeighted<T>(IList<OptionChance<T>> options);
}
