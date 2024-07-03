using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public abstract class LoadableWeaponTests
{
    protected Weapon GetLoadableWeapon()
    {
        return new()
        {
            Damage = 50,
            Accuracy = 50,
            RateOfFire = new RateOfFire()
            {
                Min = 10,
                Max = 10
            },
            Ammo = new Ammo()
            {
                Magazines = 1,
                MagazineSize = 10
            }
        };
    }
}