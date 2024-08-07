using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestModifierApplicationModifier : BaseTestModifier
{
    public TestModifierApplicationModifier(ModifierApplication appliesAt)
    {
        AppliesAt = appliesAt;
    }

    public override ModifierApplication AppliesAt { get; }

    public override ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Chance;

    public override ModifierTarget Target { get; } = ModifierTarget.SelfWeapon;
}