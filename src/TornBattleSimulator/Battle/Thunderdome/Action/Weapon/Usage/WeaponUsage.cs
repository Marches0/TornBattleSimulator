using TornBattleSimulator.Battle.Thunderdome.Accuracy;
using TornBattleSimulator.Battle.Thunderdome.Chance;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Extensions;

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
        if (weapon.Ammo != null && weapon.Ammo.MagazineAmmoRemaining == 0)
        {
            throw new InvalidOperationException("Cannot use loaded weapon without ammo.");
        }

        List<ThunderdomeEvent> events = _modifierApplier.ApplyPreActionModifiers(context, active, other, weapon.Modifiers);

        DamageResult damageResult = _damageCalculator.CalculateDamage(context, active, other, weapon);
        double hitChance = _accuracyCalculator.GetAccuracy(active, other, weapon);

        if (_chanceSource.Succeeds(hitChance))
        {
            other.Health.CurrentHealth -= damageResult.Damage;

            events.Add(context.CreateEvent(
                active,
                ThunderdomeEventType.AttackHit,
                new AttackHitEvent(weapon.Type, damageResult.Damage, damageResult.BodyPart, damageResult.Flags, hitChance))
            );
        }
        else
        {
            events.Add(context.CreateEvent(
                active,
                ThunderdomeEventType.AttackMiss,
                new AttackMissedEvent(weapon.Type, hitChance))
            );
        }

        events.AddRange(_modifierApplier.ApplyPostActionModifiers(context, active, other, weapon.Modifiers));

        if (weapon.Ammo != null)
        {
            int ammoConsumed = Random.Shared.Next(weapon.Description.RateOfFire.Min, weapon.Description.RateOfFire.Max + 1);
            weapon.Ammo!.MagazineAmmoRemaining = Math.Max(0, weapon.Ammo.MagazineAmmoRemaining - ammoConsumed);
        }

        return events;
    }
}