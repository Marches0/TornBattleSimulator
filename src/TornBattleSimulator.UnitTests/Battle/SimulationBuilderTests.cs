using FluentAssertions;
using TornBattleSimulator.Battle.Config;
using TornBattleSimulator.Input;
using TornBattleSimulator.Input.Build;
using TornBattleSimulator.Input.Build.Gear;
using TornBattleSimulator.Input.Build.Stats;

namespace TornBattleSimulator.UnitTests.Battle;

[TestFixture]
public class SimulationBuilderTests
{
    [TestCaseSource(nameof(Prepare_GivenBase_OverridesAllEmptyProperties_TestCases))]
    public void Prepare_GivenBase_OverridesAllEmptyProperties((BuildInput derived, string testName) testData)
    {
        // Arrange
        BuildInput baseBuild = GetCompleteBuild();

        // Act
        SimulatorInput config = SimulationBuilder.Prepare(new SimulatorInput() { Builds = [baseBuild, testData.derived] });

        // Assert
        testData.derived.Should().BeEquivalentTo(baseBuild);
    }

    private static IEnumerable<(BuildInput derived, string testName)> Prepare_GivenBase_OverridesAllEmptyProperties_TestCases()
    {
        yield return (new(), "Empty");
        yield return (new() { BattleStats = new() }, "Empty child object");
    }

    private BuildInput GetCompleteBuild()
    {
        return new BuildInput()
        {
            Name = "Full",
            BattleStats = new BattleStatsInput()
            {
                Strength = 1,
                Defence = 2,
                Dexterity = 3,
                Speed = 4
            },
            Primary = new WeaponInput()
            {
                Accuracy = 10,
                Damage = 20,
                Ammo = new AmmoInput()
                {
                    Magazines = 2,
                    MagazineSize = 10
                },
                RateOfFire = new RateOfFireInput()
                {
                    Min = 1,
                    Max = 5
                }
            },
            Secondary = new WeaponInput()
            {
                Accuracy = 10,
                Damage = 20,
                Ammo = new AmmoInput()
                {
                    Magazines = 2,
                    MagazineSize = 10
                },
                RateOfFire = new RateOfFireInput()
                {
                    Min = 1,
                    Max = 5
                }
            },
            Melee = new WeaponInput()
            {
                Accuracy = 10,
                Damage = 20
            }
        };
    }
}