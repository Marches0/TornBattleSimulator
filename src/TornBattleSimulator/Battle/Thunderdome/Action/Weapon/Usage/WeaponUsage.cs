using TornBattleSimulator.Battle.Thunderdome.Accuracy;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Charge;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

public class WeaponUsage : IWeaponUsage
{
    private readonly IDamageCalculator _damageCalculator;
    private readonly IAccuracyCalculator _accuracyCalculator;
    private readonly ModifierRoller _modifierRoller;
    private readonly IChanceSource _chanceSource;
    private readonly AttackModifierApplier _attackModifierApplier;
    private readonly IAmmoCalculator _ammoCalculator;

    public WeaponUsage(
        IDamageCalculator damageCalculator,
        IAccuracyCalculator accuracyCalculator,
        ModifierRoller modifierRoller,
        IChanceSource chanceSource,
        AttackModifierApplier attackModifierApplier,
        IAmmoCalculator ammoCalculator)
    {
        _damageCalculator = damageCalculator;
        _accuracyCalculator = accuracyCalculator;
        _modifierRoller = modifierRoller;

        _chanceSource = chanceSource;
        _attackModifierApplier = attackModifierApplier;
        _ammoCalculator = ammoCalculator;
    }

    public List<ThunderdomeEvent> UseWeapon(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        active.ActiveWeapon = weapon;

        List<ThunderdomeEvent> events = UseWeapon(context, active, other, weapon, false);

        Func<List<ThunderdomeEvent>> bonusAttackAction = () => UseWeapon(context, active, other, weapon, true);
        foreach (IAttacksModifier attackModifier in active.Modifiers.Active.OfType<IAttacksModifier>())
        {
            events.AddRange(
                _attackModifierApplier.MakeBonusAttacks(attackModifier, context, active, other, weapon, bonusAttackAction)
            );
        }

        foreach (ChargedModifierContainer charge in weapon.Modifiers.ChargeModifiers)
        {
            charge.Charged = false;
        }

        return events;
    }

    private List<ThunderdomeEvent> UseWeapon(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        bool bonusAction)
    {
        if (weapon.Ammo != null && weapon.Ammo.MagazineAmmoRemaining == 0)
        {
            throw new InvalidOperationException("Cannot use loaded weapon without ammo.");
        }

        List<ThunderdomeEvent> events = new();
        AttackContext attack = new AttackContext(context, active, other, weapon, null);

        // Modifiers can't trigger on bonus actions
        if (!bonusAction)
        {
            events.AddRange(
                _modifierRoller.ApplyPreActionModifiers(attack)
            );
        }

        attack.AttackResult = CalculateAttack(context, active, other, weapon);
        events.Add(MakeHit(attack));

        if (!bonusAction)
        {
            events.AddRange(_modifierRoller.ApplyPostActionModifiers(attack));
        }

        if (weapon.Ammo != null)
        {
            weapon.Ammo.MagazineAmmoRemaining = _ammoCalculator.GetAmmoRemaining(active, weapon);
        }

        foreach (var postAttackAction in active.Modifiers.Active.Concat(weapon.Modifiers.Active).OfType<IPostAttackBehaviour>())
        {
            events.AddRange(postAttackAction.PerformAction(attack));
        }

        return events;
    }

    private ThunderdomeEvent MakeHit(AttackContext attack)
    {
        // Non-damaging temps are handled specially, since they don't
        // actually miss and are better with a description of their type.
        if (attack.Weapon.Type == WeaponType.Temporary && attack.Weapon.Description.Damage == 0)
        {
            return attack.Context.CreateEvent(
               attack.Active,
               ThunderdomeEventType.UsedTemporary,
               new UsedTemporaryEvent(attack.Weapon.Description.TemporaryWeaponType!.Value)
           );
        }

        if (attack.AttackResult!.Hit)
        {
            attack.Other.Health.CurrentHealth -= attack.AttackResult.Damage.DamageDealt;

            return attack.Context.CreateEvent(
                attack.Active,
                ThunderdomeEventType.AttackHit,
                new AttackHitEvent(attack.Weapon.Type, attack.AttackResult)
            );
        }
        else
        {
            return attack.Context.CreateEvent(
                attack.Active,
                ThunderdomeEventType.AttackMiss,
                new AttackMissedEvent(attack.Weapon.Type, attack.AttackResult.HitChance)
            );
        }
    }

    private AttackResult CalculateAttack(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        double hitChance = _accuracyCalculator.GetAccuracy(active, other, weapon);
        return _chanceSource.Succeeds(hitChance)
            ? new AttackResult(true, hitChance, _damageCalculator.CalculateDamage(context, active, other, weapon))
            : new AttackResult(false, hitChance, null);
    }
}