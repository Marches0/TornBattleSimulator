using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Stackable;

public interface IStackableModifier
{
    int MaxStacks { get; }
}

public interface IStackableStatModifier : IStatsModifier, IStackableModifier
{

}