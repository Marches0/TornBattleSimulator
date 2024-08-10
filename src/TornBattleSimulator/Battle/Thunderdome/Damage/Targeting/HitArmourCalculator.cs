using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;

public class HitArmourCalculator : IHitArmourCalculator
{
    private readonly IChanceSource _chanceSource;

    public HitArmourCalculator(
        IChanceSource chanceSource)
    {
        _chanceSource = chanceSource;
    }

    public ArmourContext? GetHitArmour(
        AttackContext attack,
        BodyPart struckPart)
    {
        // If multiple pieces of armour cover the same part of the body,
        // only the strongest one is considered for mitigation.
        // https://www.torn.com/forums.php#/p=threads&f=3&t=16231895&b=0&a=0&to=21594158
        var applicableArmour = attack.Other.ArmourSet.Armour
            .Select(a => new
            {
                Armour = a,
                ApplicableCoverage = a.Coverage.FirstOrDefault(c => c.BodyPart == struckPart)
            })
            .Where(x => x.ApplicableCoverage != null)
            .MaxBy(x => x.Armour.Rating);

        if (applicableArmour == null)
        {
            return null;
        }

        // Roll against the armour's coverage to see if we hit something that is covered,
        // or a bare part.
        return _chanceSource.Succeeds(applicableArmour.ApplicableCoverage!.Coverage)
            ? applicableArmour.Armour
            : null;
    }
}