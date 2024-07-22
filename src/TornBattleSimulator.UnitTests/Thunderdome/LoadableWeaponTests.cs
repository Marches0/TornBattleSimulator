using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public abstract class LoadableWeaponTests
{
    protected WeaponContext GetLoadableWeapon()
    {
        return new WeaponContextBuilder()
            .WithAmmo(1, 10)
            .WithRateOfFire(10, 10)
            .Build();
    }
}