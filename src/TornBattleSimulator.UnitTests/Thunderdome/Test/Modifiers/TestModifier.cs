using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestModifier : IModifier
{
    public TestModifier(ModifierLifespanDescription lifespan)
    {
        Lifespan = lifespan;
    }

    public ModifierLifespanDescription Lifespan { get; }

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => ModifierTarget.Self;

    public ModifierApplication AppliesAt => ModifierApplication.BeforeAction;

    public ModifierType Effect => ModifierType.Gassed;

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.None;
}

public class TestDoTModifier : IDamageOverTimeModifier
{
    public TestDoTModifier(ModifierLifespanDescription lifespan)
    {
        Lifespan = lifespan;
    }

    public double Decay => 0.5;

    public ModifierLifespanDescription Lifespan { get; }

    public bool RequiresDamageToApply => true;

    public ModifierTarget Target => ModifierTarget.Other;

    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.SevereBurning;

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.None;
}