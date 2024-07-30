using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.BonusModifiers.DamageOverTime;

public class SevereBurningModifier : IDamageOverTimeModifier
{
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Turns(3);

    public bool RequiresDamageToApply { get; } = true;

    public ModifierTarget Target { get; } = ModifierTarget.Other;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    public ModifierType Effect { get; } = ModifierType.SevereBurning;

    public double Decay { get; } =0.45d;

    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Chance;
}