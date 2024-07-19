using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.DamageOverTime;

public class BurningModifier : IDamageOverTimeModifier
{
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Turns(3);

    public bool RequiresDamageToApply => true;

    public ModifierTarget Target => ModifierTarget.Other;

    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Burning;

    public double Decay => 0.45d;
}