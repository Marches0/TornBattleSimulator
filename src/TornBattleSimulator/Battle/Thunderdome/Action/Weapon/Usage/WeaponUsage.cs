using TornBattleSimulator.Battle.Thunderdome.Accuracy;
using TornBattleSimulator.Battle.Thunderdome.Chance;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Extensions;
using TornBattleSimulator.Battle.Thunderdome.Events;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Attacks;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

public class WeaponUsage : IWeaponUsage
{
    private readonly IDamageCalculator _damageCalculator;
    private readonly IAccuracyCalculator _accuracyCalculator;
    private readonly ModifierApplier _modifierApplier;
    private readonly IChanceSource _chanceSource;

    public WeaponUsage(
        IDamageCalculator damageCalculator,
        IAccuracyCalculator accuracyCalculator,
        ModifierApplier modifierApplier,
        IChanceSource chanceSource)
    {
        _damageCalculator = damageCalculator;
        _accuracyCalculator = accuracyCalculator;
        _modifierApplier = modifierApplier;
        _chanceSource = chanceSource;
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
                attackModifier.MakeAttack(context, active, other, weapon, bonusAttackAction)
            );
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

        List<ThunderdomeEvent> events = _modifierApplier.ApplyPreActionModifiers(context, active, other, weapon.Modifiers, bonusAction);

        DamageResult damageResult = _damageCalculator.CalculateDamage(context, active, other, weapon);
        double hitChance = _accuracyCalculator.GetAccuracy(active, other, weapon);

        events.Add(MakeHit(context, active, other, weapon, damageResult, hitChance));

        events.AddRange(_modifierApplier.ApplyPostActionModifiers(context, active, other, weapon.Modifiers, bonusAction, damageResult));

        if (weapon.Ammo != null)
        {
            int ammoConsumed = Random.Shared.Next(weapon.Description.RateOfFire.Min, weapon.Description.RateOfFire.Max + 1);
            weapon.Ammo!.MagazineAmmoRemaining = Math.Max(0, weapon.Ammo.MagazineAmmoRemaining - ammoConsumed);
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