using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestDamageModifier : BaseTestModifier, IDamageModifier
{
    private readonly double _multipler;

    public TestDamageModifier(
        double multipler,
        StatModificationType type)
    {
        _multipler = multipler;
        Type = type;
    }

    public StatModificationType Type { get; }

    public DamageModifierResult GetDamageModifier(PlayerContext active, PlayerContext other, WeaponContext weapon, DamageContext damageContext)
    {
        return new(_multipler);
    }
}