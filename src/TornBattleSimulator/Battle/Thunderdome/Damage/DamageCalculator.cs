using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
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

        Dictionary<ModificationType, List<IDamageModifier>> modifiers = _damageModifiers
            // Weapon's active modifiers (e.g. Cupid) are active.
            .Concat(weapon.Modifiers.Active.OfType<IDamageModifier>())

            // Player damage buffs are active.
            .Concat(active.Modifiers.Active.OfType<IDamageModifier>())

            // Enemy damage debuffs are active.
            //.Concat(other.Modifiers.Active.Where(m => m.Target == ModifierTarget.Other).OfType<IDamageModifier>()) // what is this
            .GroupBy(m => m.Type)
            .ToDictionary(m => m.Key, m => m.ToList());

        double baseDamageBonus = modifiers.ContainsKey(ModificationType.Additive)
            ? modifiers[ModificationType.Additive].Aggregate(0d, (total, modifier) => total + modifier.GetDamageModifier(active, other, weapon, damageContext))
            : 1d;

        // We always have multiplicate modifiers, since those are the always-applicable ones (e.g. Strength ratio)
        var damage = modifiers[ModificationType.Multiplicative]
            .Aggregate(baseDamageBonus,
                (total, modifier) => total *= modifier.GetDamageModifier(active, other, weapon, damageContext)
            );

        var actualDamage = Math.Clamp((int)Math.Round(damage), 0, other.Health.CurrentHealth);

        return new DamageResult(actualDamage, damageContext.TargetBodyPart.Value, damageContext.Flags);
    }
}