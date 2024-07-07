using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;
using TornBattleSimulator.Battle.Thunderdome.Chance;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.UnitTests.Chance;

namespace TornBattleSimulator.UnitTests.Thunderdome.Actions;

[TestFixture]
public class WeaponUsageTests : LoadableWeaponTests
{
    private readonly IChanceSource _alwaysSucceed = new FixedChanceSource(true);
    private readonly IChanceSource _alwaysFail = new FixedChanceSource(false);

    [Test]
    public void UseLoadedWeapon_OnHit_ReducesCurrentMagazineAmmoAndOtherHealth()
    {
        // Arrange
        int expectedDamage = 100;

        using AutoFake autoFake = new AutoFake();
        autoFake.Provide<IDamageCalculator>(new StaticDamageCalculator(expectedDamage));
        autoFake.Provide(_alwaysSucceed);

        PlayerContext attacker = new PlayerContextBuilder().WithPrimary(GetLoadableWeapon()).Build();
        PlayerContext defender = new PlayerContextBuilder().WithHealth(500).Build();

        WeaponUsage weaponUsage = autoFake.Resolve<WeaponUsage>();

        // Act
        attacker.Weapons.Primary!.Ammo.MagazineAmmoRemaining.Should().Be(attacker.Weapons.Primary.Ammo.MagazineSize);
        weaponUsage.UseWeapon(new ThunderdomeContext(attacker, defender), attacker, defender, attacker.Weapons.Primary);

        // Assert
        using (new AssertionScope())
        {
            attacker.Weapons.Primary!.Ammo.MagazineAmmoRemaining.Should().Be(0);
            defender.Health.CurrentHealth.Should().Be(400);
        }
    }

    [Test]
    public void UseLoadedWeapon_OnMiss_ReducesCurrentMagazineAmmoLeavesHealth()
    {
        // Arrange
        int expectedDamage = 100;

        using AutoFake autoFake = new AutoFake();
        autoFake.Provide<IDamageCalculator>(new StaticDamageCalculator(expectedDamage));
        autoFake.Provide(_alwaysFail);

        PlayerContext attacker = new PlayerContextBuilder().WithPrimary(GetLoadableWeapon()).Build();
        PlayerContext defender = new PlayerContextBuilder().WithHealth(500).Build();

        WeaponUsage weaponUsage = autoFake.Resolve<WeaponUsage>();

        // Act
        attacker.Weapons.Primary!.Ammo.MagazineAmmoRemaining.Should().Be(attacker.Weapons.Primary.Ammo.MagazineSize);
        weaponUsage.UseWeapon(new ThunderdomeContext(attacker, defender), attacker, defender, attacker.Weapons.Primary);

        // Assert
        using (new AssertionScope())
        {
            attacker.Weapons.Primary!.Ammo.MagazineAmmoRemaining.Should().Be(0);
            defender.Health.CurrentHealth.Should().Be(500);
        }
    }

    [Test]
    public void UseUnloadedWeapon_ReducesHealth()
    {
        // Arrange
        int expectedDamage = 100;

        using AutoFake autoFake = new AutoFake();
        autoFake.Provide<IDamageCalculator>(new StaticDamageCalculator(expectedDamage));
        autoFake.Provide(_alwaysSucceed);

        PlayerContext attacker = new PlayerContextBuilder().WithMelee(new Weapon() { Damage = 100, Accuracy = 100}).Build();
        PlayerContext defender = new PlayerContextBuilder().WithHealth(500).Build();

        WeaponUsage weaponUsage = autoFake.Resolve<WeaponUsage>();

        // Act
        weaponUsage.UseWeapon(new ThunderdomeContext(attacker, defender), attacker, defender, attacker.Weapons.Melee!);

        // Assert
        using (new AssertionScope())
        {
            defender.Health.CurrentHealth.Should().Be(400);
        }
    }
}