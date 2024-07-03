using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon;
using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.UnitTests.Thunderdome.Actions;

[TestFixture]
public class AttackLoadedTests : LoadableWeaponTests
{
    [Test]
    public void AttackPrimary_ReducesCurrentMagazineAmmoAndOtherHealth()
    {
        // Arrange
        int expectedDamage = 100;

        using AutoFake autoFake = new AutoFake();
        autoFake.Provide<IDamageCalculator>(new StaticDamageCalculator(expectedDamage));

        PlayerContext attacker = new PlayerContextBuilder().WithPrimary(GetLoadableWeapon()).Build();
        PlayerContext defender = new PlayerContextBuilder().WithHealth(500).Build();

        AttackPrimaryAction attack = autoFake.Resolve<AttackPrimaryAction>();

        // Act
        attacker.Primary!.Ammo.MagazineAmmoRemaining.Should().Be(attacker.Primary.Ammo.MagazineSize);
        attack.PerformAction(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        using (new AssertionScope())
        {
            attacker.Primary!.Ammo.MagazineAmmoRemaining.Should().Be(0);
            defender.Health.Should().Be(400);
        }   
    }

    [Test]
    public void AttackSecondary_ReducesCurrentMagazineAmmoAndOtherHealth()
    {
        // Arrange
        int expectedDamage = 100;

        using AutoFake autoFake = new AutoFake();
        autoFake.Provide<IDamageCalculator>(new StaticDamageCalculator(expectedDamage));

        PlayerContext attacker = new PlayerContextBuilder().WithSecondary(GetLoadableWeapon()).Build();
        PlayerContext defender = new PlayerContextBuilder().WithHealth(500).Build();

        AttackSecondaryAction attack = autoFake.Resolve<AttackSecondaryAction>();

        // Act
        attacker.Secondary!.Ammo.MagazineAmmoRemaining.Should().Be(attacker.Secondary.Ammo.MagazineSize);
        attack.PerformAction(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        using (new AssertionScope())
        {
            attacker.Secondary!.Ammo.MagazineAmmoRemaining.Should().Be(0);
            defender.Health.Should().Be(400);
        }
    }
}