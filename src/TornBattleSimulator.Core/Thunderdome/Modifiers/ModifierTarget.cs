namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

/// <summary>
///  Who a modifier will be applied to.
/// </summary>
public enum ModifierTarget
{
    /// <summary>
    ///  The modifier will be applied to the player who owns the modifier,
    ///  such as a self-buff.
    /// </summary>
    Self = 1,

    /// <summary>
    ///  The modifier will be applied to the player who does not own the modifier,
    ///  such as a debuff.
    /// </summary>
    Other
}