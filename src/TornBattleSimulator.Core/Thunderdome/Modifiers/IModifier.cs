using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

/// <summary>
///  A modifier which can be applied to a player.
/// </summary>
public interface IModifier
{
    /// <summary>
    ///  How long the modifier lasts for.
    /// </summary>
    ModifierLifespanDescription Lifespan { get; }

    /// <summary>
    ///  Whether the modifier requires a damaging hit to be applied.
    /// </summary>
    bool RequiresDamageToApply { get; }

    /// <summary>
    ///  The player that this modifier will be applied to.
    /// </summary>
    ModifierTarget Target { get; }

    /// <summary>
    ///  When this modifier becomes active.
    /// </summary>
    ModifierApplication AppliesAt { get; }

    /// <summary>
    ///  The effect that this modifier applies.
    /// </summary>
    public ModifierType Effect { get; }

    /// <summary>
    ///  How this modifier interacts with its associated value.
    /// </summary>
    public ModifierValueBehaviour ValueBehaviour { get; }
}