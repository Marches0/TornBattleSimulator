using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestModifier : BaseTestModifier
{
    public TestModifier(ModifierLifespanDescription lifespan)
    {
        Lifespan = lifespan;
    }

    public override ModifierLifespanDescription Lifespan { get; }
}