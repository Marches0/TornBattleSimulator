namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

public class PotentialModifier
{
    public PotentialModifier(
        IModifier modifier,
        double chance)
    {
        Modifier = modifier;
        Chance = chance;
    }

    public IModifier Modifier { get; }
    public double Chance { get; }
}