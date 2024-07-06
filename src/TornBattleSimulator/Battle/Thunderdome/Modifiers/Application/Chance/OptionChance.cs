namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application.Chance;

public class OptionChance<T>
{
    public OptionChance(T option, double chance)
    {
        Option = option;
        Chance = chance;
    }

    public T Option { get; }

    public double Chance { get; }
}