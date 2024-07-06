﻿using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Options;

namespace TornBattleSimulator.Battle.Thunderdome.Player.Armours;

public class ArmourFactory
{
    Dictionary<string, ArmourCoverageOption> _armourCoverage;

    public ArmourFactory(
        List<ArmourCoverageOption> armourCoverage)
    {
        _armourCoverage = armourCoverage
            .ToDictionary(c => c.Name, StringComparer.InvariantCultureIgnoreCase);
    }

    public ArmourSetContext Create(ArmourSet armourSet)
    {
        List<ArmourContext?> set = [
            Create(armourSet.Helmet),
            Create(armourSet.Body),
            Create(armourSet.Pants),
            Create(armourSet.Gloves),
            Create(armourSet.Boots),
        ];

        return new ArmourSetContext(set.Where(a => a != null).ToList()!);
    }

    private ArmourContext? Create(Armour? armour)
    {
        if (armour == null)
        {
            return null;
        }

        return new ArmourContext(armour.Rating, _armourCoverage[armour.Name].Coverage, new List<PotentialModifier>());
    }
}