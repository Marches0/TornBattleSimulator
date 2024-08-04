using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome;

/// <summary>
///  An attack being made.
/// </summary>
public class AttackContext
{
    public AttackContext(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        AttackResult? attackResult)
    {
        Context = context;
        Active = active;
        Other = other;
        Weapon = weapon;
        AttackResult = attackResult;
    }

    /// <summary>
    ///  The fight context.
    /// </summary>
    public ThunderdomeContext Context { get; }

    /// <summary>
    ///  The player making the attack.
    /// </summary>
    public PlayerContext Active { get; }

    /// <summary>
    ///  The player being attacked.
    /// </summary>
    public PlayerContext Other { get; }

    /// <summary>
    ///  The weapon being used in the attack.
    /// </summary>
    public WeaponContext Weapon { get; }

    /// <summary>
    ///  The result of the attack.
    /// </summary>
    /// <remarks>
    ///  <see langword="null"/> in the pre-attack phase, before damage and hit has been calculated.
    /// </remarks>
    public AttackResult? AttackResult { get; set; }
}