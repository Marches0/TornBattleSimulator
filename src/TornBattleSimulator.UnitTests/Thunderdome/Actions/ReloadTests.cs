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

        attacker.Primary!.Ammo.MagazineAmmoRemaining = 0;
        int startMagazines = attacker.Primary.Ammo.MagazinesRemaining;

        // Act
        action.PerformAction(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        using (new AssertionScope())
        {
            attacker.Primary.Ammo.MagazineAmmoRemaining.Should().Be(attacker.Primary.Ammo.MagazineSize);
            attacker.Primary.Ammo.MagazinesRemaining.Should().Be(startMagazines - 1);
        }
    }

    [Test]
    public void ReloadSecondaryAction_ReloadsSecondaryWeapon()
    {
        // Arrange
        PlayerContext attacker = new PlayerContextBuilder().WithSecondary(GetLoadableWeapon()).Build();
        PlayerContext defender = new PlayerContextBuilder().Build();

        ReloadSecondaryAction action = new();

        attacker.Secondary!.Ammo.MagazineAmmoRemaining = 0;
        int startMagazines = attacker.Secondary.Ammo.MagazinesRemaining;

        // Act
        action.PerformAction(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        using (new AssertionScope())
        {
            attacker.Secondary.Ammo.MagazineAmmoRemaining.Should().Be(attacker.Secondary.Ammo.MagazineSize);
            attacker.Secondary.Ammo.MagazinesRemaining.Should().Be(startMagazines - 1);
        }
    }
}