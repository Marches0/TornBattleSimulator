using FluentAssertions;
using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage.Modifiers;

[TestFixture]
public class StrengthDamageModifierTests
{
    private readonly StrengthDamageModifier _strengthDamageModifier = new();

    [TestCaseSource(nameof(StrengthDamageModifier_BasedOnStrength_ReturnsDamage_TestCases))]
    public void StrengthDamageModifier_BasedOnStrength_ReturnsDamage((ulong strength, double damage) testData)
    {
        // Arrange
        PlayerContext attacker = new PlayerContextBuilder().WithStats(new BattleStats() { Strength = testData.strength }).Build();
        PlayerContext defender = new PlayerContextBuilder().Build();

        // Act
        double damage = _strengthDamageModifier.GetDamageModifier(attacker, defender);

        // Assert
        damage.Should().BeApproximately(testData.damage, 0.0001);
    }

    private static IEnumerable<(ulong strength, double damage)> StrengthDamageModifier_BasedOnStrength_ReturnsDamage_TestCases()
    {
        // https://www.torn.com/forums.php#/p=threads&f=61&t=16199413&b=0&a=0
        const double critMultiplier = 3.5;
        // The quoted values are for crits (3.5x damage)
        // StrengthDamageModifier doesn't apply a body multiplier, so the values are scaled down.
        yield return (10, 30 / critMultiplier);
        yield return (100, 64 / critMultiplier);
        yield return (1_000, 112 / critMultiplier);
        yield return (10_000, 174 / critMultiplier);
        yield return (100_000, 250 / critMultiplier);
        yield return (1_000_000, 340 / critMultiplier);
        yield return (10_000_000, 444 / critMultiplier);
        yield return (100_000_000, 562 / critMultiplier);
        yield return (1_000_000_000, 694 / critMultiplier);
        yield return (10_000_000_000, 840 / critMultiplier);
        yield return (100_000_000_000, 1000 / critMultiplier);
    }
}