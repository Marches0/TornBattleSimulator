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

    public void Tick(ThunderdomeContext thunderdomeContext)
    {
        --_turns;
    }
}