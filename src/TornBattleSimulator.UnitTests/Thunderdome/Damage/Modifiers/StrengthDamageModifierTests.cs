using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage.Modifiers;

[TestFixture]
public class StrengthDamageModifierTests
{
    private readonly StrengthDamageModifier _strengthDamageModifier = new();

    [TestCaseSource(nameof(StrengthDamageModifier_BasedOnStrength_ReturnsMultiplier_TestCases))]
    public void StrengthDamageModifier_BasedOnStrength_ReturnsMultiplier((ulong strength, double damage) testData)
    {
        // Arrange
        PlayerContext attacker = new PlayerContextBuilder().WithStats(new BattleStats() { Strength = testData.strength }).Build();
        PlayerContext defender = new PlayerContextBuilder().Build();

        // Act
        double multiplier = _strengthDamageModifier.GetDamageModifier(attacker, defender, null, null).Multiplier;

        // Assert
        multiplier.Should().BeApproximately(testData.damage, 0.0001);
    }

    private static IEnumerable<(ulong strength, double damage)> StrengthDamageModifier_BasedOnStrength_ReturnsMultiplier_TestCases()
    {
        // https://www.torn.com/forums.php#/p=threads&f=61&t=16199413&b=0&a=0
        yield return (10, GetDamage(30));
        yield return (100, GetDamage(64));
        yield return (1_000, GetDamage(112));
        yield return (10_000, GetDamage(174));
        yield return (100_000, GetDamage(250));
        yield return (1_000_000, GetDamage(340));
        yield return (10_000_000, GetDamage(444));
        yield return (100_000_000, GetDamage(562));
        yield return (1_000_000_000, GetDamage(694));
        yield return (10_000_000_000, GetDamage(840));
        yield return (100_000_000_000, GetDamage(1000));

        double GetDamage(int quoted)
        {
            // The quoted values are for crits (3.5x damage)
            // StrengthDamageModifier doesn't apply a body multiplier, so the values are scaled down.
            const double critMultiplier = 3.5;
            return quoted / critMultiplier;
        }
    }
}