using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.BonusModifiers.DamageOverTime;

public class LacerationModifier : IDamageOverTimeModifier
{
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Turns(9);

    public bool RequiresDamageToApply { get; } = true;

    public ModifierTarget Target { get; } = ModifierTarget.Other;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    public ModifierType Effect { get; } = ModifierType.Laceration;

    public double Decay { get; } =0.9d;

    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Chance;
}