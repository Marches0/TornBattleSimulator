namespace TornBattleSimulator.Shared.Thunderdome.Modifiers.DamageOverTime;

public interface IDamageOverTimeModifier : IModifier
{
    double Decay { get; }
}