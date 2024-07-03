using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

namespace TornBattleSimulator.UnitTests.Thunderdome.Actions;

[TestFixture]
public class ReloadTests : LoadableWeaponTests
{
    [Test]
    public void ReloadPrimaryAction_ReloadsPrimaryWeapon()
    {
        // Arrange
        PlayerContext attacker = new PlayerContextBuilder().WithPrimary(GetLoadableWeapon()).Build();
        PlayerContext defender = new PlayerContextBuilder().Build();

        ReloadPrimaryAction action = new();

        attacker.Weapons.Primary!.Ammo.MagazineAmmoRemaining = 0;
        int startMagazines = attacker.Weapons.Primary.Ammo.MagazinesRemaining;

        // Act
        action.PerformAction(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        using (new AssertionScope())
        {
            attacker.Weapons.Primary.Ammo.MagazineAmmoRemaining.Should().Be(attacker.Weapons.Primary.Ammo.MagazineSize);
            attacker.Weapons.Primary.Ammo.MagazinesRemaining.Should().Be(startMagazines - 1);
        }
    }

    [Test]
    public void ReloadSecondaryAction_ReloadsSecondaryWeapon()
    {
        // Arrange
        PlayerContext attacker = new PlayerContextBuilder().WithSecondary(GetLoadableWeapon()).Build();
        PlayerContext defender = new PlayerContextBuilder().Build();

        ReloadSecondaryAction action = new();

        attacker.Weapons.Secondary!.Ammo.MagazineAmmoRemaining = 0;
        int startMagazines = attacker.Weapons.Secondary.Ammo.MagazinesRemaining;

        // Act
        action.PerformAction(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        using (new AssertionScope())
        {
            attacker.Weapons.Secondary.Ammo.MagazineAmmoRemaining.Should().Be(attacker.Weapons.Secondary.Ammo.MagazineSize);
            attacker.Weapons.Secondary.Ammo.MagazinesRemaining.Should().Be(startMagazines - 1);
        }
    }
}