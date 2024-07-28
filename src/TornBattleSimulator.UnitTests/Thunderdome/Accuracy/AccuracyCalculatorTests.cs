using FakeItEasy;
using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Accuracy;
using TornBattleSimulator.Battle.Thunderdome.Accuracy.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Accuracy;

[TestFixture]
public class AccuracyCalculatorTests
{
    [Test]
    public void AccuracyCalculator_PassesStatAccuracyToWeaponAccuracy()
    {
        const double statAccuracy = 0.1234;

        var statModifier = A.Fake<ISpeedDexterityAccuracyModifier>();
        var accuracyModifier = A.Fake<IWeaponAccuracyModifier>();

        A.CallTo(() => statModifier.GetHitChance(A<PlayerContext>._, A<PlayerContext>._))
            .Returns(statAccuracy);

        var calc = new AccuracyCalculator(statModifier, accuracyModifier);

        var mod = calc.GetAccuracy(new PlayerContextBuilder().Build(), new PlayerContextBuilder().Build(), new WeaponContextBuilder().Build());

        // Assert
        A.CallTo(() => accuracyModifier.GetHitChance(A<PlayerContext>._, A<PlayerContext>._, A<WeaponContext>._, statAccuracy))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void AccuracyCalculator_AccountsForModifiers()
    {
        // Arrange
        const double baseAccuracy = 0.5;

        var statModifier = A.Fake<ISpeedDexterityAccuracyModifier>();
        var accuracyModifier = A.Fake<IWeaponAccuracyModifier>();

        A.CallTo(() => accuracyModifier.GetHitChance(A<PlayerContext>._, A<PlayerContext>._, A<WeaponContext>._, A<double>._))
            .Returns(baseAccuracy);

        var weapon = new WeaponContextBuilder()
            .WithModifier(new TestAccuracyModifier(0.9))
            .Build();

        var attacker = new PlayerContextBuilder()
            .WithPrimary(weapon)
            .Build();

        attacker.Modifiers.AddModifier(new TestAccuracyModifier(0.4), null);

        var calc = new AccuracyCalculator(statModifier, accuracyModifier);

        // Act
        var mod = calc.GetAccuracy(attacker, new PlayerContextBuilder().Build(), weapon);

        // Assert
        // 0.5 * 0.9 * 0.4
        mod.Should().BeApproximately(0.18, 0.0001);
    }
}