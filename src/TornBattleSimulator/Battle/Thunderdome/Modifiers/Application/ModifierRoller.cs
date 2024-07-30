using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;
using System.Linq;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;

public class ModifierRoller
{
    private readonly IChanceSource _modifierChanceSource;
    private readonly IModifierApplier _modifierApplier;

    public ModifierRoller(
        IChanceSource modifierChanceSource,
        IModifierApplier modifierApplier)
    {
        _modifierChanceSource = modifierChanceSource;
        _modifierApplier = modifierApplier;
    }

    public List<ThunderdomeEvent> ApplyPreActionModifiers(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        return ApplyModifiers(
            context,
            active,
            other,
            weapon.PotentialModifiers,
            null,
            ModifierApplication.BeforeAction
        );
    }

    public List<ThunderdomeEvent> ApplyPostActionModifiers(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageResult damageResult)
    {
        List<ThunderdomeEvent> events = new();

        events.AddRange(ApplyModifiers(
            context,
            active,
            other,
            weapon.PotentialModifiers,
            damageResult,
            ModifierApplication.AfterAction
        ));

        // Some heals are actioned in the PostAction phase,
        // since they require damage data to be run.
        events.AddRange(_modifierApplier.ApplyOtherHeals(context, active, other, weapon, damageResult));

        return events;
    }

    private List<ThunderdomeEvent> ApplyModifiers(ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        List<PotentialModifier> potentialModifiers,
        DamageResult? damageResult,
        ModifierApplication modifierApplication)
    {
        List<ThunderdomeEvent> events = new();

        IEnumerable<IModifier> triggeredModifiers = potentialModifiers
            .Where(m => _modifierChanceSource.Succeeds(m.Chance))
            .Select(m => m.Modifier) // Make it an IModifier early so we don't have to remember it's Potential
            .Where(m => m.AppliesAt == modifierApplication)
            .Where(m => SatisfiesCondition(m, active, other));

        if (damageResult != null && damageResult.Damage <= 0)
        {
            triggeredModifiers = triggeredModifiers
                .Where(m => !m.RequiresDamageToApply);
        }

        foreach (IModifier modifier in triggeredModifiers)
        {
            events.AddRange(_modifierApplier.ApplyModifier(modifier, context, active, other, damageResult));
        }

        return events;
    }

    private bool SatisfiesCondition(
        IModifier modifier,
        PlayerContext active,
        PlayerContext other)
    {
        if (!(modifier is IConditionalModifier conditional))
        {
            return true;
        }

        return conditional.CanActivate(active, other);
    }
}