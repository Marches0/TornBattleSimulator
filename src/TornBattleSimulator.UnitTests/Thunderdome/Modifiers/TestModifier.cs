using TornBattleSimulator.Shared.Build.Equipment;
using TornBattleSimulator.Shared.Thunderdome.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;
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
}