using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestDamageModifier : BaseTestModifier, IDamageModifier
{
    private readonly double _multipler;
    private readonly BodyPart? _targetBodyPart;

    public TestDamageModifier(
        double multipler,
        ModificationType type,
        BodyPart? targetBodyPart = null)
    {
        _multipler = multipler;
        Type = type;
        _targetBodyPart = targetBodyPart;
    }

    public ModificationType Type { get; }

    public double GetDamageModifier(PlayerContext active, PlayerContext other, WeaponContext weapon, DamageContext damageContext)
    {
        if (_targetBodyPart.HasValue)
        {
            damageContext.TargetBodyPart = _targetBodyPart;
        }

        return _multipler;
    }
}