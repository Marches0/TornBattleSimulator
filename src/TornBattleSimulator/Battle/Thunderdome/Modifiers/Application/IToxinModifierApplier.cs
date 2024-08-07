using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;

public interface IToxinModifierApplier
{
    IModifier GetModifier();
}