namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

/// <summary>
///  A <see cref="ModifierLifespanType.AfterNextEnemyAction"/> <see cref="IModifierLifespan"/>.
/// </summary>
public class NextEnemyActionLifespan : IModifierLifespan
{
    public bool Expired { get; private set; }

    public float Remaining => throw new NotImplementedException();

    /// <inheritdoc/>
    public void FightBegin(ThunderdomeContext context) { }

    /// <inheritdoc/>
    public void OpponentActionComplete(ThunderdomeContext context)
    {
        Expired = true;
    }

    /// <inheritdoc/>
    public void OwnActionComplete(ThunderdomeContext context) { }

    /// <inheritdoc/>
    public void TurnComplete(ThunderdomeContext context) { }
}