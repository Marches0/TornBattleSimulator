﻿using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon;
using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.UnitTests.Thunderdome.Actions;

[TestFixture]
public class AttackActionTests : LoadableWeaponTests
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
        attacker.Weapons.Primary!.Ammo.MagazineAmmoRemaining.Should().Be(attacker.Weapons.Primary.Ammo.MagazineSize);
        attack.PerformAction(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        using (new AssertionScope())
        {
            attacker.Weapons.Primary!.Ammo.MagazineAmmoRemaining.Should().Be(0);
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
        attacker.Weapons!.Secondary!.Ammo.MagazineAmmoRemaining.Should().Be(attacker.Weapons.Secondary.Ammo.MagazineSize);
        attack.PerformAction(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        using (new AssertionScope())
        {
            attacker.Weapons.Secondary!.Ammo.MagazineAmmoRemaining.Should().Be(0);
            defender.Health.Should().Be(400);
        }
    }

    [Test]
    public void AttackMelee_ReducesHealth()
    {
        // Arrange
        int expectedDamage = 100;

        using AutoFake autoFake = new AutoFake();
        autoFake.Provide<IDamageCalculator>(new StaticDamageCalculator(expectedDamage));

        PlayerContext attacker = new PlayerContextBuilder().WithMelee(new Weapon() { Damage = 100, Accuracy = 100}).Build();
        PlayerContext defender = new PlayerContextBuilder().WithHealth(500).Build();

        AttackMeleeAction attack = autoFake.Resolve<AttackMeleeAction>();

        // Act
        attack.PerformAction(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        using (new AssertionScope())
        {
            defender.Health.Should().Be(400);
        }
    }
}