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
            weapon,
            null,
            ModifierApplication.BeforeAction
        );
    }

    public List<ThunderdomeEvent> ApplyPostActionModifiers(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        AttackResult attackResult)
    {
        List<ThunderdomeEvent> events = new();

        events.AddRange(ApplyModifiers(
            context,
            active,
            other,
            weapon,
            attackResult,
            ModifierApplication.AfterAction
        ));

        // Some heals are actioned in the PostAction phase,
        // since they require damage data to be run.
        events.AddRange(_modifierApplier.ApplyOtherHeals(context, active, other, weapon, attackResult));

        return events;
    }

    private List<ThunderdomeEvent> ApplyModifiers(ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        AttackResult? attackResult,
        ModifierApplication modifierApplication)
    {
        List<ThunderdomeEvent> events = new();

        IEnumerable<IModifier> triggeredModifiers = weapon.PotentialModifiers
            .Where(m => _modifierChanceSource.Succeeds(m.Chance))
            .Select(m => m.Modifier) // Make it an IModifier early so we don't have to remember it's Potential
            .Where(m => m.AppliesAt == modifierApplication)
            .Where(m => SatisfiesCondition(m, active, other, attackResult));

        if (attackResult?.Damage != null && attackResult.Damage.DamageDealt <= 0)
        {
            triggeredModifiers = triggeredModifiers
                .Where(m => !m.RequiresDamageToApply);
        }

        foreach (IModifier modifier in triggeredModifiers)
        {
            // Make an AttackContext to wrap these all up?
            events.AddRange(_modifierApplier.ApplyModifier(
                modifier,
                context,
                active,
                other,
                weapon,
                attackResult)
            );
        }

        return events;
    }

    private bool SatisfiesCondition(
        IModifier modifier,
        PlayerContext active,
        PlayerContext other,
        AttackResult? attack)
    {
        return modifier is IConditionalModifier conditional
            ? conditional.CanActivate(active, other, attack)
            : true;
    }
}