﻿using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.Core.Thunderdome.Player.Weapons;

/// <summary>
///  A weapon currently being used in a fight.
/// </summary>
public class WeaponContext : ITickable
{
    public WeaponContext(
        Weapon weapon,
        WeaponType weaponType,
        List<PotentialModifier> modifiers)
    {
        Description = weapon;
        Type = weaponType;
        Ammo = weapon.Ammo != null ? new CurrentAmmo()
        {
            Magazines = weapon.Ammo.Magazines,
            MagazinesRemaining = weapon.Ammo.Magazines,

            MagazineSize = weapon.Ammo.MagazineSize,
            MagazineAmmoRemaining = weapon.Ammo.MagazineSize
        } : null;

        Modifiers = modifiers;
    }

    public Weapon Description { get; }
    public WeaponType Type { get; }
    public CurrentAmmo? Ammo { get; }
    public bool CanReload => Ammo?.MagazinesRemaining > 0;
    public bool RequiresReload => Ammo?.MagazineAmmoRemaining == 0;

    public ModifierContext ActiveModifiers { get; set; }

    public List<PotentialModifier> Modifiers { get; }

    /// <inheritdoc/>
    public void OpponentActionComplete(ThunderdomeContext context)
    {
        ActiveModifiers.OpponentActionComplete(context);
    }

    /// <inheritdoc/>
    public void OwnActionComplete(ThunderdomeContext context)
    {
        ActiveModifiers.OwnActionComplete(context);
    }

    /// <inheritdoc/>
    public void TurnComplete(ThunderdomeContext context)
    {
        ActiveModifiers.TurnComplete(context);
    }
}