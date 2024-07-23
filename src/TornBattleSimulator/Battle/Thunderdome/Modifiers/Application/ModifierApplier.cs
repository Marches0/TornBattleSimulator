using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;

public class ModifierApplier
{
    private readonly IChanceSource _modifierChanceSource;

    public ModifierApplier(IChanceSource modifierChanceSource)
    {
        _modifierChanceSource = modifierChanceSource;
    }

    public List<ThunderdomeEvent> ApplyPreActionModifiers(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        List<PotentialModifier> potentialModifiers,
        bool bonusAction)
    {
        return ApplyModifiers(
            context,
            active,
            other,
            potentialModifiers,
            bonusAction,
            null,
            ModifierApplication.BeforeAction
        );
    }

    public List<ThunderdomeEvent> ApplyPostActionModifiers(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        List<PotentialModifier> potentialModifiers,
        bool bonusAction,
        DamageResult damageResult)
    {
        return ApplyModifiers(
            context,
            active,
            other,
            potentialModifiers,
            bonusAction,
            damageResult,
            ModifierApplication.AfterAction
        );
    }

    private List<ThunderdomeEvent> ApplyModifiers(ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        List<PotentialModifier> potentialModifiers,
        bool bonusAction,
        DamageResult? damageResult,
        ModifierApplication modifierApplication)
    {
        List<ThunderdomeEvent> events = new();

        var triggeredModifiers = potentialModifiers
            .Where(m => m.Modifier.AppliesAt == modifierApplication)
            .Where(m => _modifierChanceSource.Succeeds(m.Chance));

        if (bonusAction)
        {
            // Attacks modifiers (ones that let you attack multiple times)
            // cannot be triggered in bonus actions (since they going on already).
            triggeredModifiers = triggeredModifiers
                .Where(m => m.Modifier is not IAttacksModifier);
        }

        foreach (PotentialModifier modifier in triggeredModifiers)
        {
            PlayerContext target = modifier.Modifier.Target == ModifierTarget.Self
                ? active
                : other;

            if (target.Modifiers.AddModifier(modifier.Modifier, damageResult))
            {
                events.Add(context.CreateEvent(target, ThunderdomeEventType.EffectBegin, new EffectBeginEvent(modifier.Modifier.Effect)));
            };

            // Should this be somewhere else?
            // Post action health mods (Hardened) are applied immediately.
            // So only triggering it on application seems alright.
            if (modifier.Modifier is IHealthModifier healthModifier)
            {
                events.Add(Heal(context, target, damageResult, modifier.Modifier, healthModifier));
            }
        }

        return events;
    }

    // move this calc into seperate module
    private ThunderdomeEvent Heal(ThunderdomeContext context, PlayerContext target, DamageResult? damageResult, IModifier modifier, IHealthModifier healthModifier)
    {
        // Don't allow healing above max.
        int heal = Math.Min(
            healthModifier.GetHealthMod(target, damageResult),
            target.Health.MaxHealth - target.Health.CurrentHealth
        );

        target.Health.CurrentHealth += heal;
        return context.CreateEvent(target, ThunderdomeEventType.Heal, new HealEvent(heal, modifier.Effect));
    }
}