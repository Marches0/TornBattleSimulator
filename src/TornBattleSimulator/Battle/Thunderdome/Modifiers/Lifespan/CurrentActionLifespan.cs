namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

public class CurrentActionLifespan : IModifierLifespan
{
    public bool Expired { get; private set; } = false;

    public float Remaining => 1;

    public void OpponentActionComplete(ThunderdomeContext context) { }

    public void OwnActionComplete(ThunderdomeContext context)
    {
        Expired = true;
    }

    public void TurnComplete(ThunderdomeContext context) { }
}