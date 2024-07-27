using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Health;

/// <summary>
///  A modifier that changes a target's health.
/// </summary>
public interface IHealthModifier : IModifier
{
    /// <summary>
    ///  Get the health change.
    /// </summary>
    /// <param name="target">The target of the modifier.</param>
    /// <param name="damage">The damage done by the applying attack, if applicable.</param>
    /// <returns>The amount <paramref name="target"/>'s health will change by.</returns>
    int GetHealthModifier(PlayerContext target, DamageResult? damage);
}