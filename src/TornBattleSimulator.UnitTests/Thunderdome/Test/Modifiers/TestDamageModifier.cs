using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Thunderdome;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestDamageModifier : BaseTestModifier, IDamageModifier
{
    private readonly double _multipler;

    public TestDamageModifier(
        double multipler,
        ModificationType type)
    {
        _multipler = multipler;
        Type = type;
    }

    public ModificationType Type { get; }

    public double GetDamageModifier(AttackContext attack, HitLocation hitLocation)
    {
        return _multipler;
    }
}