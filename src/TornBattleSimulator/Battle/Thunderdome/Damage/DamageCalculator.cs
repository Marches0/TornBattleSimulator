using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Battle.Thunderdome.Damage;

public class DamageCalculator : IDamageCalculator
{
    // https://www.torn.com/forums.php#/p=threads&f=61&t=16180034&b=0&a=0
    private readonly List<IDamageModifier> _damageModifiers;
    private readonly IDamageTargeter _damageTargeter;

    public DamageCalculator(
        IEnumerable<IDamageModifier> damageModifiers,
        IDamageTargeter damageTargeter)
    {
        _damageModifiers = damageModifiers.ToList();
        _damageTargeter = damageTargeter;
    }

    public DamageResult CalculateDamage(AttackContext attack)
    {
        HitLocation hitLocation = _damageTargeter.GetDamageTarget(attack);

        // add armour bonuses too
        Dictionary<ModificationType, List<IDamageModifier>> modifiers = _damageModifiers
            // Weapon's active modifiers (e.g. Cupid) are active.
            .Concat(attack.Weapon.Modifiers.Active.OfType<IDamageModifier>())

            // Player damage buffs are active.
            .Concat(attack.Active.Modifiers.Active.OfType<IDamageModifier>())

            .GroupBy(m => m.Type)
            .ToDictionary(m => m.Key, m => m.ToList());

        // ARMOUR BONUS MODS ONLY TRIGGER IF THEY ARE THE STRUCK PIECE.
        // Calculate the targetted body part + struck armour piece to 
        // pass down

        double baseDamageBonus = modifiers.ContainsKey(ModificationType.Additive)
            ? modifiers[ModificationType.Additive].Aggregate(0d, (total, modifier) => total + modifier.GetDamageModifier(attack, hitLocation))
            : 1d;

        // We always have multiplicate modifiers, since those are the always-applicable ones (e.g. Strength ratio)
        var damage = modifiers[ModificationType.Multiplicative]
            .Aggregate(baseDamageBonus,
                (total, modifier) => total *= modifier.GetDamageModifier(attack, hitLocation)
            );

        var actualDamage = Math.Clamp((int)Math.Round(damage), 0, attack.Other.Health.CurrentHealth);

        return new DamageResult(actualDamage, hitLocation.BodyPartStruck, hitLocation.ArmourStruck != null ? DamageFlags.HitArmour : DamageFlags.MissedArmour);
    }
}