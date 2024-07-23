using TornBattleSimulator.Options;
using TornBattleSimulator.Shared.Build.Equipment;
using TornBattleSimulator.Shared.Thunderdome.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Player.Armours;

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

        return new ArmourContext(armour.Rating / 100d, _armourCoverage[armour.Name].Coverage, new List<PotentialModifier>());
    }
}