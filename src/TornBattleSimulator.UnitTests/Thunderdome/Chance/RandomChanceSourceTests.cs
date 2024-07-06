using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Chance;
using TornBattleSimulator.Battle.Thunderdome.Chance.Source;

namespace TornBattleSimulator.UnitTests.Thunderdome.Chance;

[TestFixture]
public class RandomChanceSourceTests
{
    [TestCaseSource(nameof(Succeeds_BasedOnRollVsProbability_ReturnsValue_TestData))]
    public void Succeeds_WhenRollBelowProbability_ReturnsValue((double chance, double roll, bool succeeds) testData)
    {
        // Arrange
        IRandomSource randomSource = new FixedSource(testData.roll);

        RandomChanceSource chanceSource = new RandomChanceSource(randomSource);

        chanceSource.Succeeds(testData.chance).Should().Be(testData.succeeds);
    }

    private static IEnumerable<(double chance, double roll, bool succeeds)> Succeeds_BasedOnRollVsProbability_ReturnsValue_TestData()
    {
        // Success = random number chosen was less than or equal the chance.
        // e.g. 70% chance -> must generate 0.7 or lower.
        yield return (0.7, 0.6, true);
        yield return (0.7, 0.7, true);
        yield return (0.7, 0.8, false);
        yield return (0, 0.8, false);
        yield return (0, 0d + double.Epsilon, false);
    }

    [TestCaseSource(nameof(ChooseWeighted_BasedOnRoll_ChoosesAppropriateItem_TestData))]
    public void ChooseWeighted_BasedOnRoll_ChoosesAppropriateItem((List<OptionChance<int>> options, double roll, int expected) testData)
    {
        // Arrange
        IRandomSource randomSource = new FixedSource(testData.roll);

        RandomChanceSource chanceSource = new RandomChanceSource(randomSource);

        chanceSource.ChooseWeighted(testData.options).Should().Be(testData.expected);
    }

    private static IEnumerable<(List<OptionChance<int>> options, double roll, int expected)> ChooseWeighted_BasedOnRoll_ChoosesAppropriateItem_TestData()
    {
        yield return (
            new List<OptionChance<int>>()
            {
                new OptionChance<int>(1, 0.1),
                new OptionChance<int>(2, 0.9),
            },
            0.1,
            1
        );

        yield return (
            new List<OptionChance<int>>()
            {
                new OptionChance<int>(1, 0.1),
                new OptionChance<int>(2, 0.9),
            },
            0.89,
            2
        );

        yield return (
            new List<OptionChance<int>>()
            {
                new OptionChance<int>(1, 0.1),
                new OptionChance<int>(2, 0.9),
            },
            0.99,
            2
        );

        yield return (
            new List<OptionChance<int>>()
            {
                new OptionChance<int>(1, 0.1),
                new OptionChance<int>(2, 0.9),
            },
            0.11,
            2
        );

        yield return (
            new List<OptionChance<int>>()
            {
                new OptionChance<int>(1, 0.1),
                new OptionChance<int>(2, 0.1),
                new OptionChance<int>(3, 0.1),
                new OptionChance<int>(4, 0.7),
            },
            0.1,
            1
        );

        yield return (
            new List<OptionChance<int>>()
            {
                new OptionChance<int>(1, 0.1),
                new OptionChance<int>(2, 0.1),
                new OptionChance<int>(3, 0.1),
                new OptionChance<int>(4, 0.7),
            },
            0.25,
            3
        );

        yield return (
            new List<OptionChance<int>>()
            {
                new OptionChance<int>(1, 0.1),
                new OptionChance<int>(2, 0.1),
                new OptionChance<int>(3, 0.1),
                new OptionChance<int>(4, 0.7),
            },
            0.31,
            4
        );

        yield return (
            new List<OptionChance<int>>()
            {
                new OptionChance<int>(1, 0.1),
                new OptionChance<int>(2, 0.1),
                new OptionChance<int>(3, 0.1),
                new OptionChance<int>(4, 0.7),
            },
            1.0,
            4
        );
    }
}

internal class FixedSource : IRandomSource
{
    private readonly double _roll;

    public FixedSource(double roll)
    {
        _roll = roll;
    }

    public double Next() => _roll;
}
