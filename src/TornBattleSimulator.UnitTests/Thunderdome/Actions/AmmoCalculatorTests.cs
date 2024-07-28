using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.UnitTests.Chance;
using TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Actions;

[TestFixture]
public class AmmoCalculatorTests
{
    [Test]
    public void GetRemainingAmmo_DoesNotGoBelow0()
    {
        // Arrange
        int consumed = 1000;
        int magazineSize = 10;

        FixedChanceSource chanceSource = new FixedChanceSource(true, consumed);
        AmmoCalculator ammoCalculator = new AmmoCalculator(chanceSource);

        WeaponContext weapon = new WeaponContextBuilder()
            .WithAmmo(1, magazineSize)
            .WithRateOfFire(consumed, consumed)
            .Build();

        // Act
        int remaining = ammoCalculator.GetAmmoRemaining(new PlayerContextBuilder().Build(), weapon);

        // Assert
        remaining.Should().Be(0);
    }

    [Test]
    public void GetRemainingAmmo_ConsidersModifiers()
    {
        // Arrange
        int baseConsumed = 10;

        FixedChanceSource chanceSource = new FixedChanceSource(true, baseConsumed);
        AmmoCalculator ammoCalculator = new AmmoCalculator(chanceSource);

        WeaponContext weapon = new WeaponContextBuilder()
            .WithAmmo(1, 10)
            .WithRateOfFire(baseConsumed, baseConsumed)
            .WithModifier(new TestAmmoModifier(0.9))
            .WithModifier(new TestAmmoModifier(0.4))
            .Build();

        // Act
        int remaining = ammoCalculator.GetAmmoRemaining(new PlayerContextBuilder().Build(), weapon);

        // Assert
        // 3 consumed 10 * 0.9 * 0.4
        remaining.Should().Be(7);
    }
}