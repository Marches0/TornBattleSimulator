namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Charge;

public interface IChargeableModifier : IModifier
{
    bool StartsCharged { get; }
}