namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

public class ModifierLifespanDescription
{
    public ModifierLifespanType LifespanType { get; }
    public float? Duration { get; }

    public ModifierLifespanDescription(
        ModifierLifespanType lifespanType,
        float? duration = null)
    {
        LifespanType = lifespanType;
        Duration = duration;
    }
}