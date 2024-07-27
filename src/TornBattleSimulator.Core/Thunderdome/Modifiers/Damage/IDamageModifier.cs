using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;

/// <summary>
///  A modifier which affects the damage dealt by an attack.
/// </summary>
public interface IDamageModifier
{
    /// <summary>
    ///  Gets the damage modifier.
    /// </summary>
    /// <param name="active">The player taking action.</param>
    /// <param name="other">The target of the action.</param>
    /// <param name="weapon">The weapon being used for the attack.</param>
    /// <param name="damageContext">Transient damage information.</param>
    /// <returns></returns>
    DamageModifierResult GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext);
}