using Autofac.Features.Indexed;
using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

public class ModifierFactory
{
    private readonly IIndex<ModifierType, IModifier> _modifiers;

    public ModifierFactory(
        IIndex<ModifierType, IModifier> modifiers)
    {
        _modifiers = modifiers;
    }

    public PotentialModifier GetPotentialModifier(
        ModifierType modifierType,
        double percent)
    {
        return new PotentialModifier(_modifiers[modifierType], percent);
    }
}