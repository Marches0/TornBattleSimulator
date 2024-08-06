using TornBattleSimulator.BonusModifiers.Armour;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class ArmourDamageModifier : IDamageModifier
{
    private readonly IChanceSource _chanceSource;

    public ArmourDamageModifier(
        IChanceSource chanceSource)
    {
        _chanceSource = chanceSource;
    }

    /// <inheritdoc/>
    public StatModificationType Type { get; } = StatModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext)
    {
        if (damageContext.TargetBodyPart == null)
        {
            throw new InvalidOperationException("Missing target body part.");
        }

        // - roll for "does it cover"
        // or sum the coverage of each body part with the armour? since there is some overlap.

        // Armour can overlap, and each cover the same part.
        // Not sure how it works. For now, roll against them all
        // from "strongest armour" to weakest, and use the first
        // one which procs as the hit armour value.

        // only the strongest armour can mitigate
        // https://www.torn.com/forums.php#/p=threads&f=3&t=16231895&b=0&a=0&to=21594158
        var applicableArmour = other.ArmourSet.Armour
            .Select(a => new
            {
                Rating = a.Rating,
                ApplicableCoverage = a.Coverage.FirstOrDefault(c => c.BodyPart == damageContext.TargetBodyPart.Value)
            })
            .Where(x => x.ApplicableCoverage != null)
            .OrderByDescending(x => x.Rating)
            .FirstOrDefault(x => _chanceSource.Succeeds(x.ApplicableCoverage!.Coverage));

        if (applicableArmour == null)
        {
            damageContext.SetFlag(DamageFlags.MissedArmour);
            return 1d;
        }

        damageContext.SetFlag(DamageFlags.HitArmour);

        // You can't really have more than one of these, but muliply them
        // anyway because that's how we roll.
        double penetrationReduction = weapon.Modifiers.Active
            .OfType<PenetrateModifier>()
            .Aggregate(1d, (total, mod) => total *= mod.ArmourRemaining);

        return 1d - (applicableArmour.Rating * penetrationReduction);
    }
}