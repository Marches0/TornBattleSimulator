using Spectre.Console;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;
using TornBattleSimulator.Options;

namespace TornBattleSimulator.Battle.Thunderdome.Player.Armours;

public class ArmourFactory
{
    private readonly IModifierFactory _modifierFactory;
    private readonly Dictionary<string, ArmourCoverageOption> _armourCoverage;
    private readonly Dictionary<ModifierType, double> _modifierSetBonus;

    public ArmourFactory(
        List<ArmourCoverageOption> armourCoverage,
        IModifierFactory modifierFactory,
        RootConfig rootConfig)
    {
        _armourCoverage = armourCoverage
            .ToDictionary(c => c.Name, StringComparer.InvariantCultureIgnoreCase);

        _modifierFactory = modifierFactory;

        _modifierSetBonus = rootConfig.ArmourSetBonuses;
    }

    public ArmourSetContext Create(ArmourSet armourSet)
    {
        List<ModifierType> setBonuses = GetTriggeredSetBonuses(armourSet);

        List<ArmourContext?> set = [
            Create(armourSet.Helmet, setBonuses),
            Create(armourSet.Body, setBonuses),
            Create(armourSet.Pants, setBonuses),
            Create(armourSet.Gloves, setBonuses),
            Create(armourSet.Boots, setBonuses),
        ];

        return new(set.Where(a => a != null).ToList()!);
    }

    private List<ModifierType> GetTriggeredSetBonuses(ArmourSet armourSet)
    {
        List<Armour?> pieces = [
            armourSet.Helmet,
            armourSet.Body,
            armourSet.Pants,
            armourSet.Gloves,
            armourSet.Boots,
        ];

        return pieces
            .Where(p => p != null)
            .SelectMany(p => p.Modifiers)
            .GroupBy(m => m.Type)
            .Where(x => x.Count() >= 5)
            .IntersectBy(_modifierSetBonus.Keys, x => x.Key)
            .Select(m => m.Key)
            .ToList();
    }

    private ArmourContext? Create(
        Armour? armour, List<ModifierType> setBonuses)
    {
        if (armour == null)
        {
            return null;
        }

        return new ArmourContext(
            armour.Rating / 100d,
            _armourCoverage[armour.Name].Coverage,
            armour.Modifiers.Select(m =>
                _modifierFactory.GetModifier(m.Type, setBonuses.Contains(m.Type) ? m.Percent + _modifierSetBonus[m.Type] : m.Percent)
            ).ToList()
        );
    }
}