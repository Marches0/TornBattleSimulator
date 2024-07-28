using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestDoTModifier : BaseTestModifier, IDamageOverTimeModifier
{
    public TestDoTModifier(ModifierLifespanDescription lifespan)
    {
        Lifespan = lifespan;
    }

    public double Decay => 0.5;

    public override ModifierLifespanDescription Lifespan { get; }
}