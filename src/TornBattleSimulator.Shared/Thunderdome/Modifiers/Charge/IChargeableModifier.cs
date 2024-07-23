namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Charge;

public interface IChargeableModifier : IModifier
{
    bool StartsCharged { get; }
}