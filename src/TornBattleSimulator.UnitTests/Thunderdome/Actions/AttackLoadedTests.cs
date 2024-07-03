using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon;
using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.UnitTests.Thunderdome.Actions;

[TestFixture]
public class AttackLoadedTests
{
    [Test]
    public void AttackPrimary_ReducesCurrentMagazineAmmoAndOtherHealth()
    {
        // Arrange
        int expectedDamage = 100;

        using AutoFake autoFake = new AutoFake();
        autoFake.Provide<IDamageCalculator>(new StaticDamageCalculator(expectedDamage));

        PlayerContext attacker = new PlayerContextBuilder().WithPrimary(GetStockWeapon()).Build();
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

        PlayerContext attacker = new PlayerContextBuilder().WithSecondary(GetStockWeapon()).Build();
        PlayerContext defender = new PlayerContextBuilder().WithHealth(500).Build();

        AttackSecondaryAction attackPrimary = autoFake.Resolve<AttackSecondaryAction>();

        // Act
        attacker.Secondary!.Ammo.MagazineAmmoRemaining.Should().Be(attacker.Secondary.Ammo.MagazineSize);
        attackPrimary.PerformAction(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        using (new AssertionScope())
        {
            attacker.Secondary!.Ammo.MagazineAmmoRemaining.Should().Be(0);
            defender.Health.Should().Be(400);
        }
    }

    private Weapon GetStockWeapon()
    {
        return new()
        {
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