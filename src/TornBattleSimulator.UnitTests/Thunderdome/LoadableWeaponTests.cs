using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

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