using TornBattleSimulator.Shared.Build.Equipment;
using TornBattleSimulator.Shared.Thunderdome.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Charge;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome;

internal class TestChargeableModifier : IChargeableModifier, IAutoActivateModifier
{
    public TestChargeableModifier(bool startsCharged)
    {
        StartsCharged = startsCharged;
    }

    public bool StartsCharged { get; }

    public ModifierLifespanDescription Lifespan => throw new NotImplementedException();

    public bool RequiresDamageToApply => throw new NotImplementedException();

    public ModifierTarget Target => throw new NotImplementedException();

    public ModifierApplication AppliesAt => throw new NotImplementedException();

    public ModifierType Effect => throw new NotImplementedException();
}