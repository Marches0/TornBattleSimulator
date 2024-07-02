using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Config;

namespace TornBattleSimulator.UnitTests.Battle;

[TestFixture]
public class SimulationBuilderTests
{
    [TestCaseSource(nameof(Prepare_GivenBase_OverridesAllEmptyProperties_TestCases))]
    public void Prepare_GivenBase_OverridesAllEmptyProperties((BattleBuild derived, string testName) testData)
    {
        // Arrange
        BattleBuild baseBuild = GetCompleteBuild();

        // Act
        SimulatorConfig config = SimulationBuilder.Prepare(new SimulatorConfig() { Builds = [baseBuild, testData.derived] });

        // Assert
        testData.derived.Should().BeEquivalentTo(baseBuild);
    }

    private static IEnumerable<(BattleBuild derived, string testName)> Prepare_GivenBase_OverridesAllEmptyProperties_TestCases()
    {
        yield return (new(), "Empty");
        yield return (new() { BattleStats = new() }, "Empty child object");
    }

    private BattleBuild GetCompleteBuild()
    {
        return new BattleBuild()
        {
            BattleStats = new BattleStats()
            {
                Strength = 1,
                Defence = 2,
                Dexterity = 3,
                Speed = 4
            }
        };
    }
}