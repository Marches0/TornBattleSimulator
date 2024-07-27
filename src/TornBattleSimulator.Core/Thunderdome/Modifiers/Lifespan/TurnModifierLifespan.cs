namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

/// <summary>
///  A <see cref="ModifierLifespanType.Turns"/> <see cref="IModifierLifespan"/>.
/// </summary>
public class TurnModifierLifespan : IModifierLifespan
{
    private int _turns;

    public TurnModifierLifespan(int turns)
    {
        // Start at one above, because it'll be decremented at the
        // end of the turn where it was applied and that
        // shouldn't count against it.
        _turns = turns + 1;
    }

    public bool Expired => _turns <= 0;

    public float Remaining => _turns;

    public void OpponentActionComplete(ThunderdomeContext context) { }

    public void OwnActionComplete(ThunderdomeContext context) { }

    public void TurnComplete(ThunderdomeContext context)
    {
        --_turns;
    }
}