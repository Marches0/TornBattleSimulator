using TornBattleSimulator.Battle.Thunderdome.Accuracy.Modifiers;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using FluentAssertions;

namespace TornBattleSimulator.UnitTests.Thunderdome.Accuracy;

[TestFixture]
public class WeaponAccuracyModifierTests
{
    [TestCaseSource(nameof(TestCases))]
    public void WeaponAccuracyModifier_BasedOnInputs_ReturnsAppropriateModifier((double weaponAccuracy, double statAccuracy, double expected, string testName) testData)
    {
        // Arrange
        PlayerContext attacker = new PlayerContextBuilder().Build();
        PlayerContext defender = new PlayerContextBuilder().Build();

        var weapon = new WeaponContextBuilder().WithAccuracy(testData.weaponAccuracy).Build();

        // Act
        double mod = new WeaponAccuracyModifier().GetHitChance(attacker, defender, weapon, testData.statAccuracy);

        mod.Should().BeApproximately(testData.expected, 0.001);
    }

    private static IEnumerable<(double weaponAccuracy, double statAccuracy, double expected, string testName)> TestCases()
    {
        yield return (10, 0.5, 0.1, "Spd Dex Equal uses weapon accuracy");

        // 1k spd 2k dex
        yield return (10, 0.3326, 0.0665, "Speed deficit inaccurate weapon");
        yield return (50, 0.3326, 0.332632446, "Speed deficit regular weapon");
        yield return (80, 0.3326, 0.5322, "Speed deficit accurate weapon");

        // 2k spd 1k dex
        yield return (10, 0.6673, 0.4013, "Dex deficit inaccurate weapon");
        yield return (50, 0.6673, 0.6674, "Dex deficit regular weapon");
        yield return (80, 0.6673, 0.8669, "Dex deficit accurate weapon");
    }
}