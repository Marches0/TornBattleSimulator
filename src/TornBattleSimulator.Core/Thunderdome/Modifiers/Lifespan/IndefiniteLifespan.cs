namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

/// <summary>
///  A <see cref="ModifierLifespanType.Indefinite"/> <see cref="IModifierLifespan"/>.
/// </summary>
public class IndefiniteLifespan : IModifierLifespan
{
    /// <inheritdoc/>
    public bool Expired => false;

    /// <inheritdoc/>
    public float Remaining => 1;

    /// <inheritdoc/>
    public void FightBegin(ThunderdomeContext context) { }

    /// <inheritdoc/>
    public void OpponentActionComplete(ThunderdomeContext context) { }

    /// <inheritdoc/>
    public void OwnActionComplete(ThunderdomeContext context) { }

    /// <inheritdoc/>
    public void TurnComplete(ThunderdomeContext context) { }
}