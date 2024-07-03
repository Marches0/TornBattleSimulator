using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon;
using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.UnitTests.Thunderdome.Actions;

[TestFixture]
public class AttackPrimaryActionTests
{
    [Test]
    public void AttackPrimary_ReducesOtherHealth()
    {
        // Arrange
        int expectedDamage = 100;

        using AutoFake autoFake = new AutoFake();
        autoFake.Provide<IDamageCalculator>(new StaticDamageCalculator(expectedDamage));

        PlayerContext attacker = new PlayerContextBuilder().WithPrimary(GetStockWeapon()).Build();
        PlayerContext defender = new PlayerContextBuilder().WithHealth(500).Build();

        AttackPrimaryAction attackPrimary = autoFake.Resolve<AttackPrimaryAction>();

        // Act
        attackPrimary.PerformAction(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        defender.Health.Should().Be(400);
    }

    [Test]
    public void AttackPrimary_ReducesCurrentMagazineAmmo()
    {
        // Arrange
        int expectedDamage = 100;

        using AutoFake autoFake = new AutoFake();
        autoFake.Provide<IDamageCalculator>(new StaticDamageCalculator(expectedDamage));

        PlayerContext attacker = new PlayerContextBuilder().WithPrimary(GetStockWeapon()).Build();
        PlayerContext defender = new PlayerContextBuilder().WithHealth(500).Build();

        AttackPrimaryAction attackPrimary = autoFake.Resolve<AttackPrimaryAction>();

        // Act
        attacker.Primary!.Ammo.MagazineAmmoRemaining.Should().Be(attacker.Build.Primary.Ammo.MagazineSize);
        attackPrimary.PerformAction(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        attacker.Primary!.Ammo.MagazineAmmoRemaining.Should().Be(0);
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