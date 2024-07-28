using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.UnitTests.Chance;

namespace TornBattleSimulator.UnitTests.Thunderdome.Actions;

[TestFixture]
public class WeaponUsageTests : LoadableWeaponTests
{
    [Test]
    public void UseLoadedWeapon_OnHit_ReducesCurrentMagazineAmmoAndOtherHealth()
    {
        // Arrange
        int expectedDamage = 100;

        using AutoFake autoFake = new AutoFake();
        autoFake.Provide<IDamageCalculator>(new StaticDamageCalculator(expectedDamage));
        autoFake.Provide<IChanceSource>(FixedChanceSource.AlwaysSucceeds);

        var ammoCalculator = A.Fake<IAmmoCalculator>();
        A.CallTo(() => ammoCalculator.GetAmmoRemaining(A<PlayerContext>._, A<WeaponContext>._))
            .Returns(0);

        autoFake.Provide(ammoCalculator);

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
        autoFake.Provide<IChanceSource>(FixedChanceSource.AlwaysFails);

        var ammoCalculator = A.Fake<IAmmoCalculator>();
        A.CallTo(() => ammoCalculator.GetAmmoRemaining(A<PlayerContext>._, A<WeaponContext>._))
            .Returns(0);

        autoFake.Provide(ammoCalculator);

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
        autoFake.Provide<IChanceSource>(FixedChanceSource.AlwaysSucceeds);

        PlayerContext attacker = new PlayerContextBuilder().WithMelee(new WeaponContextBuilder().WithDamage(100).Build()).Build();
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