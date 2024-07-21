using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stacking;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Weapon;

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
}