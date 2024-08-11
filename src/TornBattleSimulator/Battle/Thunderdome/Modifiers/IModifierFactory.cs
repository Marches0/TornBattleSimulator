using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

public interface IModifierFactory
{
    PotentialModifier GetModifier(ModifierType modifierType, double percent);
}