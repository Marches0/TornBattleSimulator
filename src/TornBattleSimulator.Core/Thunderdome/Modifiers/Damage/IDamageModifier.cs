using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;

/// <summary>
///  A modifier which affects the damage dealt by an attack.
/// </summary>
public interface IDamageModifier
{
    /// <summary>
    /// How this modifier is applied to the target's stats.
    /// </summary>
    ModificationType Type { get; }

    /// <summary>
    ///  Gets the damage modifier.
    /// </summary>
    double GetDamageModifier(AttackContext attack, HitLocation hitLocation);
}