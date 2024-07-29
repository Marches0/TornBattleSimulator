using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;

public class ModifierApplier : IModifierApplier
{
    private readonly IHealthModifierApplier _healthModifierApplier;

    public ModifierApplier(IHealthModifierApplier healthModifierApplier)
    {
        _healthModifierApplier = healthModifierApplier;
    }

    public List<ThunderdomeEvent> ApplyModifier(
        IModifier modifier,
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        DamageResult? damageResult)
    {
        List<ThunderdomeEvent> events = new();

        PlayerContext target = modifier.Target == ModifierTarget.Self
                ? active
                : other;

        if (target.Modifiers.AddModifier(modifier, damageResult))
        {
            events.Add(context.CreateEvent(target, ThunderdomeEventType.EffectBegin, new EffectBeginEvent(modifier.Effect)));
        };

        if (modifier is IHealthModifier { AppliesOnActivation: true } healthModifier)
        {
            events.Add(_healthModifierApplier.ModifyHealth(context, target, healthModifier, damageResult));
        }

        return events;
    }

    public List<ThunderdomeEvent> ApplyOtherHeals(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageResult? damageResult)
    {
        List<ThunderdomeEvent> events = new();

        foreach (IHealthModifier heal in active.Modifiers.Active.Concat(weapon.Modifiers.Active)
            .OfType<IHealthModifier>()
            .Where(m => !m.AppliesOnActivation))
        {
            PlayerContext target = heal.Target == ModifierTarget.Self
                ? active
                : other;

            events.Add(_healthModifierApplier.ModifyHealth(context, target, heal, damageResult));
        }

        return events;
    }
}