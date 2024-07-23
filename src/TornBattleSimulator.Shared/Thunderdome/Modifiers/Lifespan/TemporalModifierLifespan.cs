namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

public class TemporalModifierLifespan : IModifierLifespan
{
    private float _remainingTimeSeconds;

    public TemporalModifierLifespan(float lifespanSeconds)
    {
        _remainingTimeSeconds = lifespanSeconds;
    }

    public bool Expired => _remainingTimeSeconds <= 0;

    public float Remaining => _remainingTimeSeconds;

    public void OpponentActionComplete(ThunderdomeContext context) { }

    public void OwnActionComplete(ThunderdomeContext context) { }

    public void TurnComplete(ThunderdomeContext context)
    {
        _remainingTimeSeconds -= context.AttackInterval;
    }
}