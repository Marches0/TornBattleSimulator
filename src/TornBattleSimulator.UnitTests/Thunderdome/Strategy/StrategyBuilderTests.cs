using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Strategy;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Description;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

namespace TornBattleSimulator.UnitTests.Thunderdome.Strategy;

[TestFixture]
public class StrategyBuilderTests
{
    [Test]
    public void StrategyBuilder_BuildStrategy_ReturnsAppropriateStrategy()
    {
        // Arrange
        BattleBuild build = new BattleBuild()
        {
            Strategy = new List<StrategyDescription>()
            {
                new()
                {
                    Weapon = WeaponType.Primary
                },
                new()
                {
                    Weapon = WeaponType.Secondary
                },
                new()
                {
                    Weapon = WeaponType.Primary
                },
                new()
                {
                    Weapon = WeaponType.Melee
                },
                new()
                {
                    Weapon = WeaponType.Secondary
                }
            }
        };

        // Act
        CompositeStrategy strategy = (CompositeStrategy)new StrategyBuilder().BuildStrategy(build);

        // Assert
        using (new AssertionScope())
        {
            strategy.Inner[0].Should().BeOfType<PrimaryWeaponStrategy>();
            strategy.Inner[1].Should().BeOfType<SecondaryWeaponStrategy>();
            strategy.Inner[2].Should().BeOfType<PrimaryWeaponStrategy>();
            strategy.Inner[3].Should().BeOfType<MeleeWeaponStrategy>();
            strategy.Inner[4].Should().BeOfType<SecondaryWeaponStrategy>();
        }
    }
}