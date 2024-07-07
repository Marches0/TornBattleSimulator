using TornBattleSimulator.Battle.Thunderdome.Chance;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Events;
using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Extensions;

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
        List<PotentialModifier> potentialModifiers)
    {
        List<ThunderdomeEvent> events = new();

        foreach (PotentialModifier modifier in potentialModifiers
            .Where(m => m.Modifier.AppliesAt == ModifierApplication.BeforeAction)
            .Where(m => _modifierChanceSource.Succeeds(m.Chance)))
        {

        }

        return events;
    }

    public List<ThunderdomeEvent> ApplyPostActionModifiers(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        List<PotentialModifier> potentialModifiers,
        DamageResult damageResult)
    {
        List<ThunderdomeEvent> events = new();
        
        foreach (PotentialModifier modifier in potentialModifiers
            .Where(m => m.Modifier.AppliesAt == ModifierApplication.AfterAction)
            .Where(m => _modifierChanceSource.Succeeds(m.Chance)))
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