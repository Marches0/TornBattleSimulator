using FluentAssertions;
using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage.Modifiers;

[TestFixture]
public class StrengthDefenceRatioModifierTests
{
    private readonly StrengthDefenceRatioDamageModifier _strengthDefenceRatioModifier = new();

    [TestCaseSource(nameof(StrengthDefenceRatioModifier_BasedOnRatio_ReturnsCorrectModifier_TestCases))]
    public void StrengthDefenceRatioModifier_BasedOnRatio_ReturnsCorrectModifier((ulong attackerStrength, ulong defenderDefence, double expectedRatio, string testName) testData)
    {
        // Arrange
        PlayerContext attacker = new PlayerContextBuilder().WithStats(new BattleStats() { Strength = testData.attackerStrength }).Build();
        PlayerContext defender = new PlayerContextBuilder().WithStats(new BattleStats() { Defence = testData.defenderDefence }).Build();

        // Act
        double mod = _strengthDefenceRatioModifier.GetDamageModifier(attacker, defender);

        // Assert
        mod.Should().BeApproximately(testData.expectedRatio, 0.0001);
    }

    private static IEnumerable<(ulong attackerStrength, ulong defenderDefence, double expectedRatio, string testName)> StrengthDefenceRatioModifier_BasedOnRatio_ReturnsCorrectModifier_TestCases()
    {
        // Inverted, since we return "how much damage is remaining" rather than
        // "how much was mitigated"
        yield return (1000, 1000, 0.5, "Equal = 50%");
        yield return (32000, 999, 1, "Str > 32x Def -> 100%");
        yield return (999, 14000, 0, "Def > 14x Str -> 0%");

        // https://wiki.torn.com/wiki/Battle_Stats#Stat_Weights
        yield return (10_000_000, 625_000, 0.9, "10%");
        yield return (10_000_000, 1_250_000, 0.8, "20%");
        yield return (10_000_000, 2_500_000, 0.7, "30%");
        yield return (10_000_000, 5_000_000, 0.6, "40%");
        yield return (10_000_000, 10_000_000, 0.5, "50%");
        yield return (10_000_000, 15_000_000, 0.4232, "57.68%");
        yield return (10_000_000, 20_000_000, 0.3686, "63.14%");
        yield return (10_000_000, 40_000_000, 0.2374, "76.26%");
        yield return (10_000_000, 130_000_000, 0.014, "98.60%");
    }
}