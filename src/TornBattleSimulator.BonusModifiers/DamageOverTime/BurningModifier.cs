using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.BonusModifiers.DamageOverTime;

public class BurningModifier : IDamageOverTimeModifier
{
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Turns(3);

    public bool RequiresDamageToApply => true;

    public ModifierTarget Target => ModifierTarget.Other;

    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Burning;

    public double Decay => 0.45d;
}