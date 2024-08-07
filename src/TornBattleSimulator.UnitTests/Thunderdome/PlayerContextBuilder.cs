﻿using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class PlayerContextBuilder
{
    private BattleStats _battleStats = new BattleStats() {};
    private int _health;
    private WeaponContext? _primary;
    private WeaponContext? _secondary;
    private WeaponContext? _melee;
    private WeaponContext? _temporary;
    private EquippedWeapons? _equippedWeapons;
    private List<ArmourContext> _armour = new();
    private int _level = 1;

    public PlayerContextBuilder WithStats(BattleStats battleStats)
    {
        _battleStats = battleStats;
        return this;
    }

    public PlayerContextBuilder WithPrimary(WeaponContext weapon)
    {
        _primary = weapon;
        return this;
    }

    public PlayerContextBuilder WithSecondary(WeaponContext weapon)
    {
        _secondary = weapon;
        return this;
    }

    public PlayerContextBuilder WithMelee(WeaponContext weapon)
    {
        _melee = weapon;
        return this;
    }

    public PlayerContextBuilder WithTemporary(WeaponContext weapon)
    {
        _temporary = weapon;
        return this;
    }

    public PlayerContextBuilder WithHealth(int health)
    {
        _health = health;
        return this;
    }

    public PlayerContextBuilder WithWeapons(EquippedWeapons weapons)
    {
        _equippedWeapons = weapons;
        return this;
    }

    public PlayerContextBuilder WithWeapon(WeaponContext weapon)
    {
        switch (weapon.Type)
        {
            case WeaponType.Primary:
                WithPrimary(weapon);
                break;

            case WeaponType.Secondary:
                WithSecondary(weapon);
                break;

            case WeaponType.Melee:
                WithMelee(weapon);
                break;

            case WeaponType.Temporary:
                WithTemporary(weapon);
                break;
        }

        return this;
    }

    public PlayerContextBuilder WithArmour(List<ArmourContext> armour)
    {
        _armour = armour;
        return this;
    }

    public PlayerContextBuilder WithLevel(int level)
    {
        _level = level;
        return this;
    }

    public PlayerContext Build()
    {
        return new PlayerContext(
            new BattleBuild()
            {
                BattleStats = _battleStats,
                Health = _health,
                Level = _level
            },
            0,
            _equippedWeapons ?? new EquippedWeapons(
                _primary,
                _secondary,
                _melee,
                _temporary),
            new ArmourSetContext(_armour),
            null
        );
    }
}