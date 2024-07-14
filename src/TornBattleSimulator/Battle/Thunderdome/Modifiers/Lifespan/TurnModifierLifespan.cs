namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

public class TurnModifierLifespan : IModifierLifespan
{
    private int _turns;

    public TurnModifierLifespan(int turns)
    {
        _turns = turns;
    }

    public bool Expired => _turns <= 0;

    public float Remaining => _turns;

    public void OpponentActionComplete(ThunderdomeContext context) { }

    public void OwnActionComplete(ThunderdomeContext context) { }

    public void Tick(ThunderdomeContext thunderdomeContext) { }

    public void TurnComplete(ThunderdomeContext context)
    {
        --_turns;
    }
}