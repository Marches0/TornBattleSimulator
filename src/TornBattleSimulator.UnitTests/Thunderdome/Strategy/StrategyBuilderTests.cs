using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Strategy;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;
using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Strategy;
using TornBattleSimulator.UnitTests.Chance;

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
        CompositeStrategy strategy = (CompositeStrategy)new StrategyBuilder(new StunStrategy(FixedChanceSource.AlwaysSucceeds)).BuildStrategy(build);

        // Assert
        using (new AssertionScope())
        {
            strategy.Inner[0].Should().BeOfType<StunStrategy>();
            strategy.Inner[1].Should().BeOfType<PrimaryWeaponStrategy>();
            strategy.Inner[2].Should().BeOfType<SecondaryWeaponStrategy>();
            strategy.Inner[3].Should().BeOfType<PrimaryWeaponStrategy>();
            strategy.Inner[4].Should().BeOfType<MeleeWeaponStrategy>();
            strategy.Inner[5].Should().BeOfType<SecondaryWeaponStrategy>();
        }
    }
}