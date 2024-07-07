namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

public class TemporalModifierLifespan : IModifierLifespan
{
    private float _remainingTimeSeconds;

    public TemporalModifierLifespan(float lifespanSeconds)
    {
        _remainingTimeSeconds = lifespanSeconds;
    }

    public bool Expired => _remainingTimeSeconds <= 0;

    public float Remaining => _remainingTimeSeconds;

    public void Tick(ThunderdomeContext thunderdomeContext)
    {
        _remainingTimeSeconds -= thunderdomeContext.AttackInterval;
    }   
}