namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

/// <summary>
///  A <see cref="ModifierLifespanType.AfterOwnAction"/> <see cref="IModifierLifespan"/>.
/// </summary>
public class OwnActionLifespan : IModifierLifespan
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