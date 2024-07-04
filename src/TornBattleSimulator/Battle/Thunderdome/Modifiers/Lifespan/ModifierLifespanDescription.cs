namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

public class ModifierLifespanDescription
{
    public static ModifierLifespanDescription Temporal(float duration)
    {
        return new ModifierLifespanDescription(ModifierLifespanType.Temporal, duration);
    }

    public ModifierLifespanType LifespanType { get; }
    public float? Duration { get; }

    private ModifierLifespanDescription(
        ModifierLifespanType lifespanType,
        float? duration = null)
    {
        LifespanType = lifespanType;
        Duration = duration;
    }
}