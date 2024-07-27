﻿namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

/// <summary>
///  When a modifier is applied.
/// </summary>
public enum ModifierApplication
{
    /// <summary>
    ///  The modifier is applied before the active player takes their action.
    /// </summary>
    /// <remarks>
    ///  Todo: Currently nothing uses this. Consider deleting.
    /// </remarks>
    BeforeAction = 1,

    /// <summary>
    ///  The modifier is applied after the active player takes their action.
    /// </summary>
    AfterAction,

    /// <summary>
    ///  The modifier is applied when the fight begins.
    /// </summary>
    FightStart
}