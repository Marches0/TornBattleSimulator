namespace TornBattleSimulator.Core.Thunderdome;

/// <summary>
///  An item that has behaviours that take place at various stages of the fight.
/// </summary>
public interface ITickable
{
    /// <summary>
    ///  Process the start of the fight.
    /// </summary>
    void FightBegin(ThunderdomeContext context);

    /// <summary>
    ///  Process the end of the turn.
    /// </summary>
    void TurnComplete(ThunderdomeContext context);

    /// <summary>
    ///  Process the player this <see cref="ITickable"/> applies to completing
    ///  their action.
    /// </summary>
    void OwnActionComplete(ThunderdomeContext context);

    /// <summary>
    ///  Process the player this <see cref="ITickable"/> does not apply to to completing
    ///  their action.
    /// </summary>
    void OpponentActionComplete(ThunderdomeContext context);
}