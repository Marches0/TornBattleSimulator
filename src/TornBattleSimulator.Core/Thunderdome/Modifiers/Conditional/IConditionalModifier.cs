using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;

/// <summary>
///  A modifier which is active under specific circumstances.
/// </summary>
public interface IConditionalModifier : IModifier
{
    /// <summary>
    ///  Tests whether or not the modifier is currently active.
    /// </summary>
    /// <param name="owner">The player the modifier applies to.</param>
    /// <param name="other">The other player.</param>
    bool IsActive(PlayerContext owner, PlayerContext other);
}