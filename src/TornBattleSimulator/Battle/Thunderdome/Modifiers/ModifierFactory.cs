using Autofac.Features.Indexed;
using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

public class ModifierFactory
{
    private readonly IIndex<WeaponModifierType, IModifier> _modifiers;

    public ModifierFactory(
        IIndex<WeaponModifierType, IModifier> modifiers)
    {
        _modifiers = modifiers;
    }

    public PotentialModifier GetPotentialModifier(
        WeaponModifierType modifierType,
        double percent)
    {
        return new PotentialModifier(_modifiers[modifierType], percent);
    }
}