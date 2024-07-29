using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestHealthModifier : BaseTestModifier, IHealthModifier
{
    public TestHealthModifier(bool appliesOnActivation)
    {
        AppliesOnActivation = appliesOnActivation;
    }

    public bool AppliesOnActivation { get; }

    public int GetHealthModifier(PlayerContext target, DamageResult? damage)
    {
        throw new NotImplementedException();
    }
}