namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

public class CurrentActionLifespan : IModifierLifespan
{
    public bool Expired { get; private set; } = false;

    public float Remaining => 1;

    public void Tick(ThunderdomeContext thunderdomeContext)
    {
        Expired = true;
    }
}