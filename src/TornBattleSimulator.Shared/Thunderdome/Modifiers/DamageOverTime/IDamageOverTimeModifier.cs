namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.DamageOverTime;

public interface IDamageOverTimeModifier : IModifier
{
    double Decay { get; }
}