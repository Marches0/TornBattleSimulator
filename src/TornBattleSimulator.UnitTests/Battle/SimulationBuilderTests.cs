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
            Health = 7500,
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
                },
                Modifiers = new List<ModifierInput>()
                {
                    new ModifierInput()
                    {
                        Percent = 10,
                        Type = "wither"
                    }
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
            },
            Temporary = "tearGas",
            Strategy = new List<StrategyInput>()
            {
                new StrategyInput()
                {
                    Weapon = "primary",
                    Reload = true,
                    Until = new List<StrategyUntilInput>()
                    {
                        new StrategyUntilInput()
                        {
                            Effect = "bleed",
                            Count = 1,
                        }
                    }
                }
            },
            Armour = new ArmourSetInput()
            {
                Body = new ArmourInput()
                {
                    Name = "wow",
                    Rating = 10,
                    Modifiers = new List<ModifierInput>()
                    {
                        new ModifierInput()
                        {
                            Type = "Cool",
                            Percent = 11,
                        }
                    }
                },
                Boots = new ArmourInput()
                {
                    Name = "wow",
                    Rating = 10,
                    Modifiers = new List<ModifierInput>()
                    {
                        new ModifierInput()
                        {
                            Type = "Cool",
                            Percent = 11,
                        }
                    }
                },
                Gloves = new ArmourInput()
                {
                    Name = "wow",
                    Rating = 10,
                    Modifiers = new List<ModifierInput>()
                    {
                        new ModifierInput()
                        {
                            Type = "Cool",
                            Percent = 11,
                        }
                    }
                },
                Helmet = new ArmourInput()
                {
                    Name = "wow",
                    Rating = 10,
                    Modifiers = new List<ModifierInput>()
                    {
                        new ModifierInput()
                        {
                            Type = "Cool",
                            Percent = 11,
                        }
                    }
                },
                Pants = new ArmourInput()
                {
                    Name = "wow",
                    Rating = 10,
                    Modifiers = new List<ModifierInput>()
                    {
                        new ModifierInput()
                        {
                            Type = "Cool",
                            Percent = 11,
                        }
                    }
                }
            }
        };
    }
}