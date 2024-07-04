namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application.Chance;

public class RandomModifierChanceSource : IModifierChanceSource
{
    public bool Succeeds(double probability) => Random.Shared.NextDouble() <= probability;
}