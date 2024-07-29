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

    /// <summary>
    ///  Whether or not this modifier is immediately applied on activation.
    /// </summary>
    /// <remarks>
    /// <para>
    ///  When <see langword="true"/>, the heal is applied immediately when the modifier itself is applied.
    ///  When <see langword="false"/>, the heal is applied in the <see cref="ModifierApplication.AfterAction"/> phase.
    /// </para>
    /// <para>
    ///  This means that modifiers which remain active for a while can control whether or not their heal
    ///  is repeatedly triggered.
    /// </para>
    /// </remarks>
    bool AppliesOnActivation { get; }
}