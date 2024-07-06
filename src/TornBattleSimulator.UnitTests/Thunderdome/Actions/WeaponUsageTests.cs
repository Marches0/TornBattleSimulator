using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;
using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.UnitTests.Thunderdome.Actions;

[TestFixture]
public class WeaponUsageTests : LoadableWeaponTests
{
    [Test]
    public void UseLoadedWeapon_ReducesCurrentMagazineAmmoAndOtherHealth()
    {
        // Arrange
        int expectedDamage = 100;

        using AutoFake autoFake = new AutoFake();
        autoFake.Provide<IDamageCalculator>(new StaticDamageCalculator(expectedDamage));

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
    public void UseUnloadedWeapon_ReducesHealth()
    {
        // Arrange
        int expectedDamage = 100;

        using AutoFake autoFake = new AutoFake();
        autoFake.Provide<IDamageCalculator>(new StaticDamageCalculator(expectedDamage));

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