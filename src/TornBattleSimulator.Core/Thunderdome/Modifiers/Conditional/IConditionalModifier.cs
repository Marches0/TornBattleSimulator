namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;

/// <summary>
///  A modifier which is active under specific circumstances.
/// </summary>
public interface IConditionalModifier : IModifier
{
    /// <summary>
    ///  Tests whether or not the modifier can activate.
    /// </summary>
    bool CanActivate(AttackContext attack);
}