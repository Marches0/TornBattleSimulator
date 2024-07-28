using TornBattleSimulator.Core.Thunderdome.Modifiers.Charge;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

internal class TestChargeableModifier : BaseTestModifier, IChargeableModifier
{
    public TestChargeableModifier(bool startsCharged)
    {
        StartsCharged = startsCharged;
    }

    public bool StartsCharged { get; }
}