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

    public List<ThunderdomeEvent> UseWeapon(AttackContext attack)
    {
        attack.Active.ActiveWeapon = attack.Weapon;

        List<ThunderdomeEvent> events = UseWeapon(attack, false);

        Func<List<ThunderdomeEvent>> bonusAttackAction = () => UseWeapon(attack, true);
        foreach (IAttacksModifier attackModifier in attack.Active.Modifiers.Active.Concat(attack.Weapon.Modifiers.Active).OfType<IAttacksModifier>())
        {
            // todo this bit use attackcontext
            events.AddRange(
                _attackModifierApplier.MakeBonusAttacks(attackModifier, attack.Context, attack.Active, attack.Other, attack.Weapon, bonusAttackAction)
            );
        }

        foreach (ChargedModifierContainer charge in attack.Weapon.Modifiers.ChargeModifiers)
        {
            charge.Charged = false;
        }

        return events;
    }

    private List<ThunderdomeEvent> UseWeapon(
        AttackContext attack,
        bool bonusAction)
    {
        if (attack.Weapon.Ammo != null && attack.Weapon.Ammo.MagazineAmmoRemaining == 0)
        {
            throw new InvalidOperationException("Cannot use loaded weapon without ammo.");
        }

        List<ThunderdomeEvent> events = new();

        // Modifiers can't trigger on bonus actions
        if (!bonusAction)
        {
            events.AddRange(
                _modifierRoller.ApplyPreActionModifiers(attack)
            );
        }

        events.Add(_damageProcessor.PerformAttack(attack));

        if (!bonusAction)
        {
            events.AddRange(_modifierRoller.ApplyPostActionModifiers(attack));
        }

        if (attack.Weapon.Ammo != null)
        {
            attack.Weapon.Ammo.MagazineAmmoRemaining = _ammoCalculator.GetAmmoRemaining(attack.Active, attack.Weapon);
        }

        attack.Active.LastAttack = attack.AttackResult;
        return events;
    }
}