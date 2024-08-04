using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Charge;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Attacks;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

public class WeaponUsage : IWeaponUsage
{
    private readonly ModifierRoller _modifierRoller;
    private readonly AttackModifierApplier _attackModifierApplier;
    private readonly IAmmoCalculator _ammoCalculator;
    private readonly DamageProcessor _damageProcessor;

    public WeaponUsage(
        ModifierRoller modifierRoller,
        AttackModifierApplier attackModifierApplier,
        IAmmoCalculator ammoCalculator,
        DamageProcessor damageProcessor)
    {
        _modifierRoller = modifierRoller;
        _attackModifierApplier = attackModifierApplier;
        _ammoCalculator = ammoCalculator;
        _damageProcessor = damageProcessor;
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
        foreach (IAttacksModifier attackModifier in active.Modifiers.Active.Concat(weapon.Modifiers.Active).OfType<IAttacksModifier>())
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

        _damageProcessor.PerformAttack(attack);

        if (!bonusAction)
        {
            events.AddRange(_modifierRoller.ApplyPostActionModifiers(attack));
        }

        if (weapon.Ammo != null)
        {
            weapon.Ammo.MagazineAmmoRemaining = _ammoCalculator.GetAmmoRemaining(active, weapon);
        }

        active.Modifiers.AttackComplete(attack);
        weapon.Modifiers.AttackComplete(attack);

        return events;
    }
}