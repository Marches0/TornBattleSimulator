using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

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

    public ModifierType Effect => 0;

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}