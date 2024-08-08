using TornBattleSimulator.Core.Build.Equipment;
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
            MagazinesRemaining = weapon.Ammo.Magazines - 1, // The intial magazine is considered loaded in

            MagazineSize = weapon.Ammo.MagazineSize,
            MagazineAmmoRemaining = weapon.Ammo.MagazineSize
        } : null;

        PotentialModifiers = modifiers;
    }

    public Weapon Description { get; }
    public WeaponType Type { get; }
    public CurrentAmmo? Ammo { get; set; }
    public bool CanReload => Ammo?.MagazinesRemaining > 0;
    public bool RequiresReload => Ammo?.MagazineAmmoRemaining == 0;

    /// <summary>
    ///  The state of this weapon's modifiers.
    /// </summary>
    public IModifierContext Modifiers { get; set; }

    /// <summary>
    ///  Modifiers that may be granted via usage of this weapon.
    /// </summary>
    public List<PotentialModifier> PotentialModifiers { get; }

    /// <inheritdoc/>
    public void FightBegin(ThunderdomeContext context)
    {
        Modifiers.FightBegin(context);
    }

    /// <inheritdoc/>
    public void OpponentActionComplete(ThunderdomeContext context)
    {
        Modifiers.OpponentActionComplete(context);
    }

    /// <inheritdoc/>
    public void OwnActionComplete(ThunderdomeContext context)
    {
        Modifiers.OwnActionComplete(context);
    }

    /// <inheritdoc/>
    public void TurnComplete(ThunderdomeContext context)
    {
        Modifiers.TurnComplete(context);
    }
}