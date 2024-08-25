using Spectre.Console;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;
using TornBattleSimulator.Options;

namespace TornBattleSimulator.Battle.Thunderdome.Player.Armours;

public class ArmourFactory
{
    private readonly IModifierFactory _modifierFactory;
    private readonly Dictionary<string, ArmourCoverageOption> _armourCoverage;
    private readonly Dictionary<ModifierType, double> _modifierSetBonus;

    // Modifiers which apply to the player as a whole, rather than
    // the piece of armour they are attached to.
    private static readonly List<ModifierType> FlatModifiers = [ModifierType.Invulnerable];

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
        List<ModifierType> bonuses = GetTriggeredSetBonuses(armourSet);

        List<ArmourContext?> set = [
            Create(armourSet.Helmet, bonuses),
            Create(armourSet.Body, bonuses),
            Create(armourSet.Pants, bonuses),
            Create(armourSet.Gloves, bonuses),
            Create(armourSet.Boots, bonuses),
        ];

        ArmourSetContext ac = new ArmourSetContext(set.Where(a => a != null).ToList()!);

        // Add flat modifiers to any piece, because it doesn't matter which one they
        // are on - they'll be applied to the player later.
        ac.Armour.First().PotentialModifiers.AddRange(GetFlatModifiers(armourSet, bonuses));
        return ac;
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
        Armour? armour,
        List<ModifierType> setBonuses)
    {
        if (armour == null)
        {
            return null;
        }

        return new ArmourContext(
            armour.Rating / 100d,
            _armourCoverage[armour.Name].Coverage,
            armour.Modifiers.Where(m => !FlatModifiers.Contains(m.Type)).Select(m =>
                _modifierFactory.GetModifier(m.Type, setBonuses.Contains(m.Type) ? m.Percent + _modifierSetBonus[m.Type] : m.Percent)
            ).ToList()
        );
    }

    private List<PotentialModifier> GetFlatModifiers(
        ArmourSet armourSet, List<ModifierType> setBonuses)
    {
        IEnumerable<ModifierDescription> empty = Enumerable.Empty<ModifierDescription>();

        IEnumerable<ModifierDescription> allModifiers = (armourSet.Helmet?.Modifiers ?? empty)
            .Concat(armourSet.Body?.Modifiers ?? empty)
            .Concat(armourSet.Pants?.Modifiers ?? empty)
            .Concat(armourSet.Gloves?.Modifiers ?? empty)
            .Concat(armourSet.Boots?.Modifiers ?? empty);

        IEnumerable<IGrouping<ModifierType, ModifierDescription>> flatModifiers = allModifiers.GroupBy(m => m.Type)
            .IntersectBy(FlatModifiers, a => a.Key);

        return flatModifiers
            .Select(m => _modifierFactory.GetModifier(
                m.Key,
                m.Sum(d => d.Percent) 
                    + (setBonuses.Contains(m.Key) ? _modifierSetBonus[m.Key] : 0)
                )
            )
            .ToList();
    }
}