﻿using System.Diagnostics.CodeAnalysis;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
public class WeaponsFactory
{
    private readonly IModifierFactory _modifierFactory;
    private readonly TemporaryWeaponFactory _temporaryWeaponFactory;

    public WeaponsFactory(
        IModifierFactory modifierFactory,
        TemporaryWeaponFactory temporaryWeaponFactory)
    {
        _modifierFactory = modifierFactory;
        _temporaryWeaponFactory = temporaryWeaponFactory;
    }

    public EquippedWeapons Create(BattleBuild build)
    {
        Weapon? temporary = build.Temporary != null
            ? _temporaryWeaponFactory.GetTemporaryWeapon(build.Temporary.Value)
            : null;

        return new EquippedWeapons(
             GetContext(build.Primary, WeaponType.Primary),
             GetContext(build.Secondary, WeaponType.Secondary),
             GetContext(build.Melee, WeaponType.Melee),
             GetContext(temporary, WeaponType.Temporary)
        );
    }

    [return: NotNullIfNotNull(nameof(weapon))]
    public WeaponContext? GetContext(Weapon? weapon, WeaponType weaponType)
    {
        if (weapon == null)
        {
            return null;
        }

        return new WeaponContext(
            weapon,
            weaponType,
            weapon.Modifiers
                .Select(m => _modifierFactory.GetModifier(m.Type, m.Percent))
                .ToList()
        );
    }
}