namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

/// <summary>
///  A modifier with a max stack of one.
/// </summary>
/// <remarks>
///  Preferred over <see cref="IStackableModifier"/> for single-stack modifiers.
/// </remarks>
public interface IExclusiveModifier : IModifier
{

}