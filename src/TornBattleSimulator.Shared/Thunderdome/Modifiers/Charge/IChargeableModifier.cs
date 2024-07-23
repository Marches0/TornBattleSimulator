namespace TornBattleSimulator.Shared.Thunderdome.Modifiers.Charge;

public interface IChargeableModifier : IModifier
{
    bool StartsCharged { get; }
}