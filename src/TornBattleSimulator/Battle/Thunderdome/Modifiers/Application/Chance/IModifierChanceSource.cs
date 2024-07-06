namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application.Chance;

public interface IModifierChanceSource
{
    bool Succeeds(double probability);
    T ChooseWeighted<T>(IList<OptionChance<T>> options);
}
