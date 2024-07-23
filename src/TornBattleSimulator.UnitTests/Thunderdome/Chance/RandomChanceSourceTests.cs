using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Chance;
using TornBattleSimulator.Battle.Thunderdome.Chance.Source;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Chance.Source;
using TornBattleSimulator.UnitTests.Chance;

namespace TornBattleSimulator.UnitTests.Thunderdome.Chance;

[TestFixture]
public class RandomChanceSourceTests
{
    private const int RandomIterations = 5000000;
    private const double RandomChanceLeeway = 0.001; // 0.1%

    [TestCaseSource(nameof(Succeeds_BasedOnRollVsProbability_ReturnsValue_TestData))]
    public void Succeeds_WhenRollBelowProbability_ReturnsValue((double chance, double roll, bool succeeds) testData)
    {
        // Arrange
        IRandomSource randomSource = new FixedRandomSource(testData.roll, 1);

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
        IRandomSource randomSource = new FixedRandomSource(testData.roll, 1);

        RandomChanceSource chanceSource = new RandomChanceSource(randomSource);

        chanceSource.ChooseWeighted(testData.options).Should().Be(testData.expected);
    }

    [Test]
    public void Success_WithRealSource_SucceedsAppropriately()
    {
        double chance = 0.25;
        RandomSource randomSource = new RandomSource();
        RandomChanceSource chanceSource = new RandomChanceSource(randomSource);

        int successes = 0;

        for(int i = 0; i < RandomIterations; i++)
        {
            if (chanceSource.Succeeds(chance))
            {
                ++successes;
            }
        }

        double observedChance = (double)successes / RandomIterations;
        observedChance.Should().BeApproximately(chance, RandomChanceLeeway);
    }

    [Test]
    public void ChooseWeighted_WithRealSource_ChoosesAppropriately()
    {
        RandomSource randomSource = new RandomSource();
        RandomChanceSource chanceSource = new RandomChanceSource(randomSource);

        List<OptionChance<int>> options = new()
        {
            new(0, 0.05),
            new(1, 0.2),
            new(2, 0.4),
            new(3, 0.1),
            new(4, 0.1),
            new(5, 0.1),
            new(6, 0.05),
        };

        List<int> results = Enumerable.Range(0, options.Count).Select(_ => 0).ToList();

        for (int i = 0; i < RandomIterations; i++)
        {
            int choice = chanceSource.ChooseWeighted(options);
            ++results[choice];
        }

        using (new AssertionScope())
        {
            foreach ((OptionChance<int> option, int index) in options.Select((o, i) => (o, i)))
            {
                var observedChance = (double)results[index] / RandomIterations;
                observedChance.Should().BeApproximately(option.Chance, RandomChanceLeeway);
            }
        }
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