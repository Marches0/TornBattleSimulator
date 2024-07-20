using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stacking;

public interface IStackableModifier
{
    int MaxStacks { get; }
}

public interface IStackableStatModifier : IStatsModifier, IStackableModifier
{

}