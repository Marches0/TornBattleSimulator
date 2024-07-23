using TornBattleSimulator.Shared.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Shared.Thunderdome.Modifiers.Stackable;

public interface IStackableModifier
{
    int MaxStacks { get; }
}

public interface IStackableStatModifier : IStatsModifier, IStackableModifier
{

}