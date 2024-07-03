﻿using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
public class WeaponsFactory
{
    private readonly ModifierFactory _modifierFactory;
    private readonly TemporaryWeaponFactory _temporaryWeaponFactory;

    public WeaponsFactory(
        ModifierFactory modifierFactory,
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

    private WeaponContext? GetContext(Weapon? weapon, WeaponType weaponType)
    {
        return weapon is null
            ? null
            : new WeaponContext(weapon, weaponType, GetModifiers(weapon));
    }

    private List<PotentialModifier> GetModifiers(Weapon weapon)
    {
        return weapon.Modifiers
            .Select(m => _modifierFactory.GetModifier(m.Type, m.Percent))
            .ToList();
    }
}