using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
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

    public ModifierType Effect => 0;

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}