using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stackable;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Stats.Weapon;

public class FreezeModifier : IStackableStatModifier
{
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    public bool RequiresDamageToApply => true;

    public ModifierTarget Target => ModifierTarget.Other;

    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Freeze;

    public int MaxStacks => 1;

    public float GetDefenceModifier() => 1;

    public float GetDexterityModifier() => 0.5f;

    public float GetSpeedModifier() => 0.5f;

    public float GetStrengthModifier() => 1;

    public StatModificationType Type => StatModificationType.Multiplicative;

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}