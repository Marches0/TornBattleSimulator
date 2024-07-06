namespace TornBattleSimulator.Battle.Thunderdome.Chance;

public interface IModifierChanceSource
{
    bool Succeeds(double probability);
    T ChooseWeighted<T>(IList<OptionChance<T>> options);
}
