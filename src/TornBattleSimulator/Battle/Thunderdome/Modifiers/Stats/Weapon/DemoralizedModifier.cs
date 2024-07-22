using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stacking;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Weapon;

public class DemoralizedModifier : IStackableStatModifier
{
    public int MaxStacks => 5;

    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Temporal(180); // I think

    public bool RequiresDamageToApply => true;

    public ModifierTarget Target => ModifierTarget.Other;

    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Demoralized;

    public float GetDefenceModifier() => 0.9f;

    public float GetDexterityModifier() => 0.9f;

    public float GetSpeedModifier() => 0.9f;

    public float GetStrengthModifier() => 0.9f;

    public StatModificationType Type => StatModificationType.Multiplicative;
}