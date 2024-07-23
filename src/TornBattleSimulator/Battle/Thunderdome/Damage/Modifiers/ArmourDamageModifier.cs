using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
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

    public DamageModifierResult GetDamageModifier(
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
        var applicableArmour = other.ArmourSet.Armour
            .Select(a => new
            {
                Rating = a.Rating,
                ApplicableCoverage = a.Coverage.FirstOrDefault(c => c.BodyPart == damageContext.TargetBodyPart.Value)
            })
            .Where(x => x.ApplicableCoverage != null)
            .OrderByDescending(x => x.Rating)
            .FirstOrDefault(x => _chanceSource.Succeeds(x.ApplicableCoverage!.Coverage));

        if (applicableArmour != null)
        {
            // https://wiki.torn.com/wiki/Armor#Advanced_Armor_Bonuses
            damageContext.SetFlag(DamageFlags.HitArmour); 
            return new DamageModifierResult(1d - applicableArmour.Rating);
        }
        else
        {
            damageContext.SetFlag(DamageFlags.MissedArmour);
            return new DamageModifierResult(1d);
        }
    }
}