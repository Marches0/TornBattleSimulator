using TornBattleSimulator.Battle.Thunderdome.Target;
using TornBattleSimulator.BonusModifiers.Stats.Weapon;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;

public class ModifierApplier : IModifierApplier
{
    private readonly IHealthModifierApplier _healthModifierApplier;
    private readonly IToxinModifierApplier _toxinModifierApplier;
    private readonly TargetSelector _targetSelector;

    public ModifierApplier(
        IHealthModifierApplier healthModifierApplier,
        IToxinModifierApplier toxinModifierApplier,
        TargetSelector targetSelector)
    {
        _healthModifierApplier = healthModifierApplier;
        _toxinModifierApplier = toxinModifierApplier;
        _targetSelector = targetSelector;
    }

    public List<ThunderdomeEvent> ApplyModifier(
        IModifier modifier,
        AttackContext attack)
    {
        // meh
        if (modifier is ToxinModifier)
        {
            return ApplyModifier(
                _toxinModifierApplier.GetModifier(),
                attack
            );
        }

        List<ThunderdomeEvent> events = new();
        (PlayerContext target, IModifierContext modifierTarget) = _targetSelector.GetModifierTarget(modifier, attack);

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
            (PlayerContext target, IModifierContext _) = _targetSelector.GetModifierTarget(heal, attack);

            events.Add(_healthModifierApplier.ModifyHealth(attack.Context, target, heal, attack.AttackResult));
        }

        return events;
    }

    public List<ThunderdomeEvent> ApplyFightStartModifiers(ThunderdomeContext context)
    {
        return ApplyFightStartModifiers(context, context.Attacker, context.Defender)
            .Concat(ApplyFightStartModifiers(context, context.Defender, context.Attacker)
            ).ToList();
    }

    private List<ThunderdomeEvent> ApplyFightStartModifiers(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        List<ThunderdomeEvent> events = new();

        if (active.Weapons.Primary != null)
        {
            events.AddRange(ApplyFightStartModifiers(context, active, active.Weapons.Primary, other));
        }

        if (active.Weapons.Secondary != null)
        {
            events.AddRange(ApplyFightStartModifiers(context, active, active.Weapons.Secondary, other));
        }

        if (active.Weapons.Melee != null)
        {
            events.AddRange(ApplyFightStartModifiers(context, active, active.Weapons.Melee, other));
        }

        if (active.Weapons.Temporary != null)
        {
            events.AddRange(ApplyFightStartModifiers(context, active, active.Weapons.Temporary, other));
        }

        foreach (ArmourContext armourPiece in active.ArmourSet.Armour)
        {
            events.AddRange(ApplyFightStartModifiers(context, active, armourPiece, other));
        }

        return events;
    }

    private List<ThunderdomeEvent> ApplyFightStartModifiers(ThunderdomeContext context, PlayerContext player, WeaponContext weapon, PlayerContext other)
    {
        // Not good. but here we are.
        weapon.Modifiers = new ModifierContext(player);

        List<ThunderdomeEvent> events = new();

        // make an attackcontext just to reuse other logic
        AttackContext attack = new AttackContext(context, player, other, weapon, null);

        foreach (var modifier in weapon.PotentialModifiers
            .Select(m => m.Modifier)
            .Where(m => m.AppliesAt == ModifierApplication.FightStart))
        {
            (PlayerContext target, IModifierContext modifierTarget) = _targetSelector.GetModifierTarget(modifier, attack);
            if (modifierTarget.AddModifier(modifier, null))
            {
                events.Add(context.CreateEvent(target, ThunderdomeEventType.EffectBegin, new EffectBeginEvent(modifier.Effect)));
            }
        }

        return events;
    }

    private List<ThunderdomeEvent> ApplyFightStartModifiers(ThunderdomeContext context, PlayerContext player, ArmourContext armour, PlayerContext other)
    {
        armour.Modifiers = new ModifierContext(player);

        List<ThunderdomeEvent> events = new();

        foreach (var modifier in armour.PotentialModifiers
            .Select(m => m.Modifier)
            .Where(m => m.AppliesAt == ModifierApplication.FightStart))
        {
            if (armour.Modifiers.AddModifier(modifier, null))
            {
                events.Add(context.CreateEvent(player, ThunderdomeEventType.EffectBegin, new EffectBeginEvent(modifier.Effect)));
            }
        }

        return events;
    }
}