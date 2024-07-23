namespace TornBattleSimulator.Core.Thunderdome.Modifiers.DamageOverTime;

public interface IDamageOverTimeModifier : IModifier
{
    double Decay { get; }
}