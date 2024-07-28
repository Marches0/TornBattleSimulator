using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

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
            // Weapon's active modifiers (e.g. Cupid) are active.
            .Concat(weapon.Modifiers.Active.Where(m => m.Target == ModifierTarget.Self).OfType<IDamageModifier>())

            // Player damage buffs are active.
            .Concat(active.Modifiers.Active.Where(m => m.Target == ModifierTarget.Self).OfType<IDamageModifier>())

            // Enemy damage debuffs are active.
            .Concat(other.Modifiers.Active.Where(m => m.Target == ModifierTarget.Other).OfType<IDamageModifier>())

            .Aggregate(new { Damage = 1d, BodyPart = (BodyPart)0 },
                (total, modifier) =>
                {
                    DamageModifierResult result = modifier.GetDamageModifier(active, other, weapon, damageContext);
                    return new { Damage = total.Damage * result.Multiplier, BodyPart = total.BodyPart | result.BodyPart };
                });

        return new DamageResult((int)Math.Round(damage.Damage), damage.BodyPart, damageContext.Flags);
    }
}