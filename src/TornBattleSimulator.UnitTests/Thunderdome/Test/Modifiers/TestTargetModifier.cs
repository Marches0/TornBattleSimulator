using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestTargetModifier : BaseTestModifier
{
    public TestTargetModifier(ModifierTarget target)
    {
        Target = target;
    }

    public override ModifierTarget Target { get; }
    public override ModifierType Effect { get; } = ModifierType.Achilles;
}