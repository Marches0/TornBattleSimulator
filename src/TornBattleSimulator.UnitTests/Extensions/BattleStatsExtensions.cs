using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Shared.Build;
using TornBattleSimulator.Shared.Extensions;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.UnitTests.Thunderdome;

namespace TornBattleSimulator.UnitTests.Extensions;

[TestFixture]
public class BattleStatsExtensions
{
    [TestCaseSource(nameof(WithModifiers_Multipliers_CalculatesCorrectly_TestCases))]
    public void WithModifiers_Multipliers_CalculatesCorrectly(
        (BattleStats baseStats, List<IStatsModifier> modifiers, BattleStats expected, string testName) testData)
    {
        var modified = testData.baseStats.WithModifiers(testData.modifiers);

        using (new AssertionScope())
        {
            modified.Strength.Should().Be(testData.expected.Strength);
            modified.Defence.Should().Be(testData.expected.Defence);
            modified.Speed.Should().Be(testData.expected.Speed);
            modified.Dexterity.Should().Be(testData.expected.Dexterity);
        }
    }

    private static IEnumerable<(BattleStats baseStats, List<IStatsModifier> modifiers, BattleStats expected, string testName)> WithModifiers_Multipliers_CalculatesCorrectly_TestCases()
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
                new TestStatMultiplier(1.4f, 1.3f, 1.2f, 1.1f, StatModificationType.Additive),
                new TestStatMultiplier(1.4f, 1.3f, 1.2f, 1.1f, StatModificationType.Additive)
            ],
            new BattleStats()
            {
                Strength = 180,
                Defence = 127,
                Speed = 84,
                Dexterity = 48
            },
            "Additive multipliers interact correctly"
        );

        yield return (
            baseStats,
            [
                new TestStatMultiplier(1.4f, 1.3f, 1.2f, 1.1f, StatModificationType.Multiplicative),
                new TestStatMultiplier(1.4f, 1.3f, 1.2f, 1.1f, StatModificationType.Multiplicative)
            ],
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
                new TestStatMultiplier(1.4f, 1.3f, 1.2f, 1.1f, StatModificationType.Additive),
                new TestStatMultiplier(1.4f, 1.3f, 1.2f, 1.1f, StatModificationType.Additive),
                new TestStatMultiplier(1.4f, 1.3f, 1.2f, 1.1f, StatModificationType.Multiplicative)
            ],
            new BattleStats()
            {
                Strength = 252,
                Defence = 165,
                Speed = 100,
                Dexterity = 52
            },
            "Additive and Multiplicative multipliers interact correctly"
        );
    }
}