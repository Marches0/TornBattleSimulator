using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.Actions;

[TestFixture]
public class ReloadActionTests : LoadableWeaponTests
{
    [Test]
    public void PerformAction_ReloadsWeapon()
    {
        WeaponContext weapon = new WeaponContextBuilder()
            .WithAmmo(3, 10)
            .Build();

        weapon.Ammo.MagazineAmmoRemaining = 0;
        int startMagazines = weapon.Ammo.MagazinesRemaining;

        AttackContext attack = new AttackContextBuilder()
            .WithWeapon(weapon)
            .Build();
        
        // Act
        new ReloadAction().PerformAction(attack);
        
        // Asset
        using (new AssertionScope())
        {
            weapon.Ammo.MagazineAmmoRemaining.Should()
                .Be(weapon.Ammo.MagazineSize);

            weapon.Ammo.MagazinesRemaining.Should()
                .Be(startMagazines - 1);
        }
    }
}