using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;

/// <summary>
///  A modifier which is active under specific circumstances.
/// </summary>
public interface IConditionalModifier : IModifier
{
    /// <summary>
    ///  Tests whether or not the modifier can activate.
    /// </summary>
    /// <param name="active">The player applying this modifier.</param>
    /// <param name="other">The other player.</param>
    /// <param name="attack">The attack creating this modifier, if applicable.</param>
    bool CanActivate(PlayerContext active, PlayerContext other, AttackResult? attack);
}