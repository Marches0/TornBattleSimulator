using Spectre.Console;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;
using TornBattleSimulator.Options;

namespace TornBattleSimulator.Battle.Thunderdome.Player.Armours;

public class ArmourFactory
{
    private readonly ModifierFactory _modifierFactory;
    private readonly Dictionary<string, ArmourCoverageOption> _armourCoverage;
    private readonly Dictionary<ModifierType, double> _modifierSetBonus;

    public ArmourFactory(
        List<ArmourCoverageOption> armourCoverage,
        ModifierFactory modifierFactory,
        RootConfig rootConfig)
    {
        _armourCoverage = armourCoverage
            .ToDictionary(c => c.Name, StringComparer.InvariantCultureIgnoreCase);

        _modifierFactory = modifierFactory;

        _modifierSetBonus = rootConfig.ArmourSetBonuses;
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

        ArmourSetContext ctx = new(set.Where(a => a != null).ToList()!);
        AddSetBonus(ctx);

        return ctx;
    }

    private void AddSetBonus(ArmourSetContext armourSetContext)
    {
        // If you have a full set of the same modifier, you get a bonus one.
        var modifiers = armourSetContext.PotentialModifiers
            .GroupBy(m => m.Modifier.Effect)
            .Where(x => x.Count() >= 5)
            .IntersectBy(_modifierSetBonus.Keys, x => x.Key)
            .Select(x => _modifierFactory.GetModifier(x.Key, _modifierSetBonus[x.Key]));

        // Just add them to the first piece of armour, since it doesn't actually matter.
        armourSetContext.Armour[0].PotentialModifiers.AddRange(modifiers);
    }

    private ArmourContext? Create(Armour? armour)
    {
        if (armour == null)
        {
            return null;
        }

        return new ArmourContext(
            armour.Rating / 100d,
            _armourCoverage[armour.Name].Coverage,
            armour.Modifiers.Select(m => _modifierFactory.GetModifier(m.Type, m.Percent)).ToList()
        );
    }
}