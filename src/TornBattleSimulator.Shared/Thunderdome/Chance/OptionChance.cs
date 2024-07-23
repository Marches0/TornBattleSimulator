namespace TornBattleSimulator.Core.Thunderdome.Chance;

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