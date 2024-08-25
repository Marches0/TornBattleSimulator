using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.BonusModifiers.Stats;
using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

namespace TornBattleSimulator.UnitTests.Extensions;

[TestFixture]
public class BattleStatsExtensions
{
    [TestCaseSource(nameof(WithModifiers_Multipliers_CalculatesCorrectly_TestCases))]
    public void WithModifiers_Multipliers_CalculatesCorrectly(
        (BattleStats baseStats, List<IStatsModifier> modifiers, IStatsModifierModifier? statsModifierModifier, BattleStats expected, string testName) testData)
    {
        var modified = testData.baseStats.WithModifiers(testData.modifiers, testData.statsModifierModifier);

        using (new AssertionScope())
        {
            modified.Strength.Should().Be(testData.expected.Strength);
            modified.Defence.Should().Be(testData.expected.Defence);
            modified.Speed.Should().Be(testData.expected.Speed);
            modified.Dexterity.Should().Be(testData.expected.Dexterity);
        }
    }

    [Test]
    public void WithModifiers_ReturnsAtLeastZero()
    {
        // Arrange
        BattleStats battleStats = new BattleStats()
        {
            Strength = 1,
            Defence = 1,
            Speed = 1,
            Dexterity = 1,
        };

        // Act
        var low = battleStats.WithModifiers(
            [new TestStatModifier(-1, -1, -1, -1, ModificationType.Additive), new TestStatModifier(-1, -1, -1, -1, ModificationType.Multiplicative)],
            null
        );

        // Assert
        using (new AssertionScope())
        {
            low.Strength.Should().Be(0);
            low.Defence.Should().Be(0);
            low.Speed.Should().Be(0);
            low.Dexterity.Should().Be(0);
        }
    }

    private static IEnumerable<(
        BattleStats baseStats,
        List<IStatsModifier> modifiers,
        IStatsModifierModifier? statsModifierModifier,
        BattleStats expected,
        string testName)> WithModifiers_Multipliers_CalculatesCorrectly_TestCases()
    {
        // Expected are slightly off the actual multiple expected results
        // due to floating point funtimes
        BattleStats baseStats = new BattleStats()
        {
            Strength = 100,
            Defence = 80,
            Speed = 60,
            Dexterity = 40
        };

        yield return (
            baseStats,
            [ 
                new TestStatModifier(1.4, 1.3, 1.2, 1.1, ModificationType.Additive),
                new TestStatModifier(1.4, 1.3, 1.2, 1.1, ModificationType.Additive)
            ],
            null,
            new BattleStats()
            {
                Strength = 179,
                Defence = 128,
                Speed = 84,
                Dexterity = 48
            },
            "Additive multipliers interact correctly"
        );

        yield return (
            baseStats,
            [
                new TestStatModifier(1.4, 1.3, 1.2, 1.1, ModificationType.Multiplicative),
                new TestStatModifier(1.4, 1.3, 1.2, 1.1, ModificationType.Multiplicative)
            ],
            null,
            new BattleStats()
            {
                Strength = 196,
                Defence = 135,
                Speed = 86,
                Dexterity = 48
            },
            "Multiplicative multipliers interact correctly"
        );

        yield return (
            baseStats,
            [
                new TestStatModifier(1.4, 1.3, 1.2, 1.1, ModificationType.Additive),
                new TestStatModifier(1.4, 1.3, 1.2, 1.1, ModificationType.Additive),
                new TestStatModifier(1.4, 1.3, 1.2, 1.1, ModificationType.Multiplicative)
            ],
            null,
            new BattleStats()
            {
                Strength = 250,
                Defence = 166,
                Speed = 100,
                Dexterity = 52
            },
            "Additive and Multiplicative multipliers interact correctly"
        );

        // Debuffs are inverted for multiplicativemodifier modifiers
        // 0.3 * 0.9 raw would actually make it stronger
        // Instead, do
        // (1 - 0.3) * 0.9 = 0.63
        // Then invert that
        // 1 - 0.63 = 0.37
        // To get the amount it's now reduced by

        // Strength Add: 1 + 0.4 - (0.3 * 0.9)
        //             = 1.13
        // Strength Mul: 1.13 * 1.4 * (0.3 @ 0.9 = 0.37)
        //             = 0.58534
        // Strength    : 100 * 0.42714
        //             = 58.534
        yield return (
            baseStats,
            [
                new TestStatModifier(1.4, 1.3, 1.2, 1.1, ModificationType.Additive),
                new TestStatModifier(0.7, 1.3, 1.2, 1.1, ModificationType.Additive),
                new TestStatModifier(1.4, 1.3, 1.2, 1.1, ModificationType.Multiplicative),
                new TestStatModifier(0.3, 1.3, 1.2, 1.1, ModificationType.Multiplicative)
            ],
            new InvulnerableModifier(0.1), // 10% reduce each
            new BattleStats()
            {
                Strength = 57,
                Defence = 215,
                Speed = 120,
                Dexterity = 57
            },
            "Additive and multiplicative multipliers with StatsModifierModifier interact correctly"
        );
    }
}