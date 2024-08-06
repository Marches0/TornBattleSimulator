﻿using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.BonusModifiers.Damage;

public class FinaleModifier : IDamageModifier, IModifier
{
    private readonly double _value;

    public FinaleModifier(double value)
    {
        _value = value;
    }

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterOwnAction);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.SelfWeapon;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.BeforeAction;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Finale;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Additive;

    /// <inheritdoc/>
    public double GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext)
    {
        // Finale: For every turn that this weapon wasn't used,
        // add a stack of value.
        List<BattleAction> weaponActions = GetUsageActions(weapon);

        int actionsSinceWeaponUsedCount = active.Actions
            .Reverse<BattleAction>() // Latest actions at end
            .TakeWhile(a => !weaponActions.Contains(a))
            .Count();

        return 1 + (actionsSinceWeaponUsedCount * _value);
    }

    private List<BattleAction> GetUsageActions(WeaponContext weapon)
    {
        return weapon.Type switch
        {
            WeaponType.Primary => [ BattleAction.AttackPrimary, BattleAction.ReloadPrimary, BattleAction.ChargePrimary ],
            WeaponType.Secondary => [BattleAction.AttackSecondary, BattleAction.ReloadSecondary, BattleAction.ChargeSecondary],
            WeaponType.Melee => [BattleAction.AttackMelee, BattleAction.ChargeMelee],
            _ => throw new ArgumentOutOfRangeException($"{weapon.Type} is not valid for {nameof(FinaleModifier)}")
        };
    }
}