namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

public class ModifierLifespanDescription
{
    public static ModifierLifespanDescription Temporal(float duration)
    {
        return new ModifierLifespanDescription(ModifierLifespanType.Temporal, duration: duration);
    }

    public static ModifierLifespanDescription Turns(int turns)
    {
        return new ModifierLifespanDescription(ModifierLifespanType.Turns, turnCount: turns);
    }

    public static ModifierLifespanDescription Fixed(ModifierLifespanType lifespanType)
    {
        return new ModifierLifespanDescription(lifespanType, null, null);
    }

    public ModifierLifespanType LifespanType { get; }
    public float? Duration { get; }

    public int? TurnCount { get; }

    private ModifierLifespanDescription(
        ModifierLifespanType lifespanType,
        float? duration = null,
        int? turnCount = null)
    {
        LifespanType = lifespanType;
        Duration = duration;
        TurnCount = turnCount;
    }
}