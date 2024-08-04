using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
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
        AttackContext attack)
    {
        List<ThunderdomeEvent> events = new();
        (PlayerContext target, IModifierContext modifierTarget) = GetTarget(modifier, attack);

        if (modifierTarget.AddModifier(modifier, attack.AttackResult))
        {
            events.Add(attack.Context.CreateEvent(target, ThunderdomeEventType.EffectBegin, new EffectBeginEvent(modifier.Effect)));
        };

        if (modifier is IHealthModifier { AppliesOnActivation: true } healthModifier)
        {
            events.Add(_healthModifierApplier.ModifyHealth(attack.Context, target, healthModifier, attack.AttackResult));
        }

        return events;
    }

    public List<ThunderdomeEvent> ApplyOtherHeals(AttackContext attack)
    {
        List<ThunderdomeEvent> events = new();

        foreach (IHealthModifier heal in attack.Active.Modifiers.Active.Concat(attack.Weapon.Modifiers.Active)
            .OfType<IHealthModifier>()
            .Where(m => !m.AppliesOnActivation))
        {
            (PlayerContext target, IModifierContext _) = GetTarget(heal, attack);

            events.Add(_healthModifierApplier.ModifyHealth(attack.Context, target, heal, attack.AttackResult));
        }

        return events;
    }

    public List<ThunderdomeEvent> ApplyFightStartModifiers(ThunderdomeContext context)
    {
        return ApplyFightStartModifiers(context, context.Attacker)
            .Concat(ApplyFightStartModifiers(context, context.Defender)
            ).ToList();
    }

    private List<ThunderdomeEvent> ApplyFightStartModifiers(ThunderdomeContext context, PlayerContext player)
    {
        List<ThunderdomeEvent> events = new();

        if (player.Weapons.Primary != null)
        {
            events.AddRange(ApplyFightStartModifiers(context, player, player.Weapons.Primary));
        }

        if (player.Weapons.Secondary != null)
        {
            events.AddRange(ApplyFightStartModifiers(context, player, player.Weapons.Secondary));
        }

        if (player.Weapons.Melee != null)
        {
            events.AddRange(ApplyFightStartModifiers(context, player, player.Weapons.Melee));
        }

        if (player.Weapons.Temporary != null)
        {
            events.AddRange(ApplyFightStartModifiers(context, player, player.Weapons.Temporary));
        }

        return events;
    }

    private List<ThunderdomeEvent> ApplyFightStartModifiers(ThunderdomeContext context, PlayerContext player, WeaponContext weapon)
    {
        // Not good. but here we are.
        weapon.Modifiers = new ModifierContext(player);

        List<ThunderdomeEvent> events = new();

        foreach (var modifier in weapon.PotentialModifiers
            .Select(m => m.Modifier)
            .Where(m => m.AppliesAt == ModifierApplication.FightStart)
            .Where(m => m.Target == ModifierTarget.Self))
        {
            if (weapon.Modifiers.AddModifier(modifier, null))
            {
                events.Add(context.CreateEvent(player, ThunderdomeEventType.EffectBegin, new EffectBeginEvent(modifier.Effect)));
            }
        }

        return events;
    }

    private (PlayerContext target, IModifierContext modifierTarget) GetTarget(
        IModifier modifier,
        AttackContext attack)
    {
        PlayerContext target = modifier.Target == ModifierTarget.Self
                ? attack.Active
                : attack.Other;

        IModifierContext contextTarget = modifier.Target == ModifierTarget.SelfWeapon
            ? attack.Weapon.Modifiers
            : target.Modifiers;

        return (target, contextTarget);
    }
}