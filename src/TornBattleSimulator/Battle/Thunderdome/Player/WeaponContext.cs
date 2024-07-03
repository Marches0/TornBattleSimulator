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
        Weapon = weapon;
        Ammo = new CurrentAmmo()
        {
            Magazines = weapon.Ammo.Magazines,
            MagazinesRemaining = weapon.Ammo.Magazines,

            MagazineSize = weapon.Ammo.MagazineSize,
            MagazineAmmoRemaining = weapon.Ammo.MagazineSize
        };
    }

    public Weapon Weapon { get; }
    public CurrentAmmo Ammo { get; }
}