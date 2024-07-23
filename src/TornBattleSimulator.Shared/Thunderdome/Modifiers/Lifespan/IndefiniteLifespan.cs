namespace TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;

public class IndefiniteLifespan : IModifierLifespan
{
    public bool Expired => false;

    public float Remaining => 1;

    public void OpponentActionComplete(ThunderdomeContext context) { }

    public void OwnActionComplete(ThunderdomeContext context) { }

    public void TurnComplete(ThunderdomeContext context) { }
}