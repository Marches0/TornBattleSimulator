namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

/// <summary>
///  The type of lifespan a modifier is measured against.
/// </summary>
public enum ModifierLifespanType
{
    /// <summary>
    ///  Measured in time.
    /// </summary>
    Temporal = 1,

    /// <summary>
    ///  Measured in turns.
    /// </summary>
    Turns,

    /// <summary>
    ///  Ends after the action of the player that applied it.
    /// </summary>
    AfterOwnAction,

    /// <summary>
    ///  Ends after the action of the other player.
    /// </summary>
    AfterNextEnemyAction,

    /// <summary>
    ///  Ends after the next action of the player that applied it.
    /// </summary>
    AfterNextOwnAction,

    /// <summary>
    ///  Ends after an external condition has been satisfied.
    /// </summary>
    Conditional,

    /// <summary>
    ///  Does not end.
    /// </summary>
    Indefinite
}