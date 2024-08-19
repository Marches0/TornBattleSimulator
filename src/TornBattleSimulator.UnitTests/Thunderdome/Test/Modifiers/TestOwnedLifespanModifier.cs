using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestOwnedLifespanModifier : BaseTestModifier, IOwnedLifespan
{
    private readonly bool _expired;

    public TestOwnedLifespanModifier(bool expired)
    {
        _expired = expired;
    }

    public override ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.Custom);

    public bool Expired(PlayerContext owner, AttackResult? attack)
    {
        return _expired;
    }
}