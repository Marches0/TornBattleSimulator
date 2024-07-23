using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Damage;
using TornBattleSimulator.Shared.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Damage;

public class DamageCalculator : IDamageCalculator
{
    // https://www.torn.com/forums.php#/p=threads&f=61&t=16180034&b=0&a=0
    private readonly List<IDamageModifier> _damageModifiers;

    public DamageCalculator(
        IEnumerable<IDamageModifier> damageModifiers)
    {
        _damageModifiers = damageModifiers.ToList();
    }

    public DamageResult CalculateDamage(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        DamageContext damageContext = new();

        var damage = _damageModifiers
            .Concat(weapon.AlwaysActiveModifiers.OfType<IDamageModifier>())
            .Aggregate(new { Damage = 1d, BodyPart = (BodyPart)0 },
                (total, modifier) =>
                {
                    DamageModifierResult result = modifier.GetDamageModifier(active, other, weapon, damageContext);
                    return new { Damage = total.Damage * result.Multiplier, BodyPart = total.BodyPart | result.BodyPart };
                });

        return new DamageResult((int)Math.Round(damage.Damage), damage.BodyPart, damageContext.Flags);
    }
}