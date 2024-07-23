using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Accuracy.Modifiers;
using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.Accuracy;

[TestFixture]
public class SpeedDexterityAccuracyModifierTests
{
    [TestCaseSource(nameof(SpeedDexterityAccuracyModifier_BasedOnRatio_ReturnsCorrectModifier_TestCases))]
    public void SpeedDexterityAccuracyModifier_BasedOnRatio_ReturnsCorrectModifier((ulong attackerSpeed, ulong defenderDexterity, double expected, string testName) testData)
    {
        // Arrange
        PlayerContext attacker = new PlayerContextBuilder().WithStats(new BattleStats() { Speed = testData.attackerSpeed }).Build();
        PlayerContext defender = new PlayerContextBuilder().WithStats(new BattleStats() { Dexterity = testData.defenderDexterity }).Build();

        // Act
        double mod = new SpeedDexterityAccuracyModifier().GetHitChance(attacker, defender);

        // Assert
        mod.Should().BeApproximately(testData.expected, 0.0001);
    }

    private static IEnumerable<(ulong attackerSpeed, ulong defenderDexterity, double expected, string testName)> SpeedDexterityAccuracyModifier_BasedOnRatio_ReturnsCorrectModifier_TestCases()
    {
        yield return (1000, 1000, 0.5, "Equal = 50%");
        yield return (64000, 999, 1, "Spd > 64x Dex -> 100%");
        yield return (999, 64000, 0, "Dex > 64x Spd -> 0%");

        // https://wiki.torn.com/wiki/Battle_Stats
        yield return (500_000, 10_000_000, 0.0563, "5.63%");
        yield return (1_000_000, 10_000_000, 0.1093, "10.93%");
        yield return (5_000_000, 10_000_000, 0.3326, "33.26%");
        yield return (15_000_000, 10_000_000, 0.6049, "60.49%");
        yield return (30_000_000, 10_000_000, 0.7415, "74.15%");
        yield return (90_000_000, 10_000_000, 0.8810, "88.10%");
    }
}