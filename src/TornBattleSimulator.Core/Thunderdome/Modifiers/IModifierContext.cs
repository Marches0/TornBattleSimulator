using System.Collections.ObjectModel;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Charge;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

/// <summary>
///  A set of tracked modifiers.
/// </summary>
public interface IModifierContext : ITickable
{
    /// <summary>
    ///  The modifiers which are currently active.
    /// </summary>
    ReadOnlyCollection<IModifier> Active { get; }

    /// <summary>
    ///  The modifiers that require charging.
    /// </summary>
    List<ChargedModifierContainer> ChargeModifiers { get; }

    /// <summary>
    ///  Adds a new modifier.
    /// </summary>
    /// <param name="modifier">The modifier to add.</param>
    /// <param name="damageResult">The damage caused by the active player, if applicable.</param>
    /// <returns><see langword="true"/> if the modifier was added, otherwise <see langword="false"/>.</returns>
    bool AddModifier(IModifier modifier, DamageResult? damageResult);
}