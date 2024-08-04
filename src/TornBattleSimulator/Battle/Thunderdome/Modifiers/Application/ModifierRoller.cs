using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;

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

    public List<ThunderdomeEvent> ApplyPreActionModifiers(AttackContext attack)
    {
        return ApplyModifiers(
            attack,
            ModifierApplication.BeforeAction
        );
    }

    public List<ThunderdomeEvent> ApplyPostActionModifiers(AttackContext attack)
    {
        List<ThunderdomeEvent> events = new();

        events.AddRange(ApplyModifiers(
            attack,
            ModifierApplication.AfterAction
        ));

        // Some heals are actioned in the PostAction phase,
        // since they require damage data to be run.
        events.AddRange(_modifierApplier.ApplyOtherHeals(attack));

        return events;
    }

    private List<ThunderdomeEvent> ApplyModifiers(
        AttackContext attack,
        ModifierApplication modifierApplication)
    {
        List<ThunderdomeEvent> events = new();

        IEnumerable<IModifier> triggeredModifiers = attack.Weapon.PotentialModifiers
            .Where(m => _modifierChanceSource.Succeeds(m.Chance))
            .Select(m => m.Modifier) // Make it an IModifier early so we don't have to remember it's Potential
            .Where(m => m.AppliesAt == modifierApplication)
            .Where(m => SatisfiesCondition(m, attack));

        if (attack.AttackResult?.Damage != null && attack.AttackResult.Damage.DamageDealt <= 0)
        {
            triggeredModifiers = triggeredModifiers
                .Where(m => !m.RequiresDamageToApply);
        }

        foreach (IModifier modifier in triggeredModifiers)
        {
            // Make an AttackContext to wrap these all up?
            events.AddRange(_modifierApplier.ApplyModifier(
                modifier,
                attack)
            );
        }

        return events;
    }

    private bool SatisfiesCondition(
        IModifier modifier,
        AttackContext attack)
    {
        return modifier is IConditionalModifier conditional
            ? conditional.CanActivate(attack.Active, attack.Other, attack.AttackResult)
            : true;
    }
}