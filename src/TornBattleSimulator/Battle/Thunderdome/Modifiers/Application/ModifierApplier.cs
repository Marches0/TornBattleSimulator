using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

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

        // All heals are actioned in the PostAction phase, since some require
        // damage data to be run.
        foreach (IHealthModifier heal in active.Modifiers.Active.Concat(weapon.Modifiers.Active)
            .OfType<IHealthModifier>()
            .Where(m => !m.AppliesOnActivation))
        {
            PlayerContext target = heal.Target == ModifierTarget.Self
                ? active
                : other;

            events.Add(Heal(context, target, damageResult, heal));
        }

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

        var triggeredModifiers = potentialModifiers
            .Where(m => m.Modifier.AppliesAt == modifierApplication)
            .Where(m => _modifierChanceSource.Succeeds(m.Chance));

        foreach (PotentialModifier modifier in triggeredModifiers)
        {
            PlayerContext target = modifier.Modifier.Target == ModifierTarget.Self
                ? active
                : other;

            if (target.Modifiers.AddModifier(modifier.Modifier, damageResult))
            {
                events.Add(context.CreateEvent(target, ThunderdomeEventType.EffectBegin, new EffectBeginEvent(modifier.Modifier.Effect)));
            };

            if (modifier.Modifier is IHealthModifier { AppliesOnActivation: true } healthModifier)
            {
                events.Add(Heal(context, target, damageResult, healthModifier));
            }
        }

        return events;
    }

    // move this calc into seperate module
    private ThunderdomeEvent Heal(ThunderdomeContext context, PlayerContext target, DamageResult? damageResult, IHealthModifier healthModifier)
    {
        // Don't allow healing above max.
        int heal = Math.Min(
            healthModifier.GetHealthModifier(target, damageResult),
            target.Health.MaxHealth - target.Health.CurrentHealth
        );

        target.Health.CurrentHealth += heal;

        return heal >= 0
            ? context.CreateEvent(target, ThunderdomeEventType.Heal, new HealEvent(heal, healthModifier.Effect))
            : context.CreateEvent(target, ThunderdomeEventType.ExtraDamage, new ExtraDamageEvent(-heal, healthModifier.Effect));
    }
}