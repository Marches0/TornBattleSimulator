namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

/// <summary>
///  How the modifier interacts with its provided percent.
/// </summary>
public enum ModifierValueBehaviour
{
    /// <summary>
    ///  The modifier has a random chance to be activated.
    /// </summary>
    Chance = 1,

    /// <summary>
    ///  The strength of the modifier is based on its provided percent.
    /// </summary>
    Potency = 2,

    /// <summary>
    ///  The modifier does not interact with a percent.
    /// </summary>
    None = 3
}