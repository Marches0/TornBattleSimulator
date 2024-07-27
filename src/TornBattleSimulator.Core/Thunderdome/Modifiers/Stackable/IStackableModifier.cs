using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Stackable;

/// <summary>
///  A modifier which can be obtained multiple times, and stack with modifiers of the same type.
/// </summary>
/// <remarks>
///  Stacking modifiers interact with each other additively.
/// </remarks>
public interface IStackableModifier
{
    /// <summary>
    ///  The maximum number of stacks this modifier can have at any given time.
    /// </summary>
    int MaxStacks { get; }
}

/// <summary>
///  A <see cref="IStackableModifier"/> for <see cref="IStatsModifier"/>.
/// </summary>
public interface IStackableStatModifier : IStatsModifier, IStackableModifier
{

}