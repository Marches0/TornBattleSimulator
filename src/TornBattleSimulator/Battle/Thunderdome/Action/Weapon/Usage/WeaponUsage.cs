using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Extensions;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

public class WeaponUsage : IWeaponUsage
{
    private readonly IDamageCalculator _damageCalculator;
    private readonly ModifierApplier _modifierApplier;

    public WeaponUsage(
        IDamageCalculator damageCalculator,
        ModifierApplier modifierApplier)
    {
        _damageCalculator = damageCalculator;
        _modifierApplier = modifierApplier;
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
        other.Health.CurrentHealth -= damageResult.Damage;

        events.Add(context.CreateEvent(
            active,
            ThunderdomeEventType.AttackHit,
            new AttackHitEvent(weapon.Type, damageResult.Damage, damageResult.BodyPart)));

        events.AddRange(_modifierApplier.ApplyPostActionModifiers(context, active, other, weapon.Modifiers));

        if (weapon.Ammo != null)
        {
            int ammoConsumed = Random.Shared.Next(weapon.Description.RateOfFire.Min, weapon.Description.RateOfFire.Max + 1);
            weapon.Ammo!.MagazineAmmoRemaining = Math.Max(0, weapon.Ammo.MagazineAmmoRemaining - ammoConsumed);
        }

        return events;
    }
}