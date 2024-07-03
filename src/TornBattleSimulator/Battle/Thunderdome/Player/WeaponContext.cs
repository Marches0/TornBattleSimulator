using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Player;
public class WeaponContext
{
    public WeaponContext(
        Weapon weapon)
    {
        Description = weapon;
        Ammo = weapon.Ammo != null ? new CurrentAmmo()
        {
            Magazines = weapon.Ammo.Magazines,
            MagazinesRemaining = weapon.Ammo.Magazines,

            MagazineSize = weapon.Ammo.MagazineSize,
            MagazineAmmoRemaining = weapon.Ammo.MagazineSize
        } : null;
    }

    public Weapon Description { get; }
    public CurrentAmmo? Ammo { get; }
    public bool CanReload => Ammo?.MagazinesRemaining > 0;
    public bool RequiresReload => Ammo?.MagazineAmmoRemaining == 0;
}