namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

/// <summary>
///  A <see cref="ModifierLifespanType.Temporal"/> <see cref="IModifierLifespan"/>.
/// </summary>
public class TemporalModifierLifespan : IModifierLifespan
{
    private float _remainingTimeSeconds;

    public TemporalModifierLifespan(float lifespanSeconds)
    {
        _remainingTimeSeconds = lifespanSeconds;
    }

    public bool Expired => _remainingTimeSeconds <= 0;

    public float Remaining => _remainingTimeSeconds;

    /// <inheritdoc/>
    public void FightBegin(ThunderdomeContext context) { }

    /// <inheritdoc/>
    public void OpponentActionComplete(ThunderdomeContext context) { }

    /// <inheritdoc/>
    public void OwnActionComplete(ThunderdomeContext context) { }

    /// <inheritdoc/>
    public void TurnComplete(ThunderdomeContext context)
    {
        _remainingTimeSeconds -= context.AttackInterval;
    }
}