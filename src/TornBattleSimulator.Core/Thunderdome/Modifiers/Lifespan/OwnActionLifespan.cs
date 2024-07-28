namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

/// <summary>
///  A <see cref="ModifierLifespanType.AfterOwnAction"/> <see cref="IModifierLifespan"/>.
/// </summary>
public class OwnActionLifespan : IModifierLifespan
{
    public bool Expired { get; private set; } = false;

    public float Remaining => 1;

    /// <inheritdoc/>
    public void FightBegin(ThunderdomeContext context) { }

    /// <inheritdoc/>
    public void OpponentActionComplete(ThunderdomeContext context) { }

    /// <inheritdoc/>
    public void OwnActionComplete(ThunderdomeContext context)
    {
        Expired = true;
    }

    /// <inheritdoc/>
    public void TurnComplete(ThunderdomeContext context) { }
}