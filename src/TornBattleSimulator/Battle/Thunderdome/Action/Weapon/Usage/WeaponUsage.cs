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

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

public class WeaponUsage : IWeaponUsage
{
    private readonly IDamageCalculator _damageCalculator;
    private readonly IAccuracyCalculator _accuracyCalculator;
    private readonly ModifierApplier _modifierApplier;
    private readonly IChanceSource _chanceSource;
    private readonly AttackModifierApplier _attackModifierApplier;
    private readonly IAmmoCalculator _ammoCalculator;

    public WeaponUsage(
        IDamageCalculator damageCalculator,
        IAccuracyCalculator accuracyCalculator,
        ModifierApplier modifierApplier,
        IChanceSource chanceSource,
        AttackModifierApplier attackModifierApplier,
        IAmmoCalculator ammoCalculator)
    {
        _damageCalculator = damageCalculator;
        _accuracyCalculator = accuracyCalculator;
        _modifierApplier = modifierApplier;
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

        // Modifiers can't trigger on bonus actions
        if (!bonusAction)
        {
            events.AddRange(
                _modifierApplier.ApplyPreActionModifiers(context, active, other, weapon.PotentialModifiers)
            );
        }

        DamageResult damageResult = _damageCalculator.CalculateDamage(context, active, other, weapon);
        double hitChance = _accuracyCalculator.GetAccuracy(active, other, weapon);

        events.Add(MakeHit(context, active, other, weapon, damageResult, hitChance));

        if (!bonusAction)
        {
            events.AddRange(_modifierApplier.ApplyPostActionModifiers(context, active, other, weapon.PotentialModifiers, damageResult));
        }

        if (weapon.Ammo != null)
        {
            weapon.Ammo!.MagazineAmmoRemaining = _ammoCalculator.GetAmmoRemaining(active, weapon);
        }

        return events;
    }

    private ThunderdomeEvent MakeHit(ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageResult damageResult,
        double hitChance)
    {
        // Non-damaging temps are handled specially, since they don't
        // actually miss and are better with a description of their type.
        if (weapon.Type == WeaponType.Temporary && weapon.Description.Damage == 0)
        {
            return context.CreateEvent(
               active,
               ThunderdomeEventType.UsedTemporary,
               new UsedTemporaryEvent(weapon.Description.TemporaryWeaponType!.Value)
           );
        }

        if (_chanceSource.Succeeds(hitChance))
        {
            other.Health.CurrentHealth -= damageResult.Damage;

            return context.CreateEvent(
                active,
                ThunderdomeEventType.AttackHit,
                new AttackHitEvent(weapon.Type, damageResult.Damage, damageResult.BodyPart, damageResult.Flags, hitChance)
            );
        }
        else
        {
            return context.CreateEvent(
                active,
                ThunderdomeEventType.AttackMiss,
                new AttackMissedEvent(weapon.Type, hitChance)
            );
        }
    }
}