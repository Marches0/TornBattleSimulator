using FakeItEasy;
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
                },
                new()
                {
                    Weapon = WeaponType.Temporary
                }
            }
        };

        // Act
        CompositeStrategy strategy = (CompositeStrategy)new StrategyBuilder(new MissTurnStrategy(FixedChanceSource.AlwaysSucceeds), A.Fake<IUntilConditionResolver>()).BuildStrategy(build);

        // Assert
        using (new AssertionScope())
        {
            strategy.Inner[0].Should().BeOfType<MissTurnStrategy>();
            strategy.Inner[1].Should().Match(s => IsUseWeaponStrategy(s, WeaponType.Primary));
            strategy.Inner[2].Should().Match(s => IsUseWeaponStrategy(s, WeaponType.Secondary));
            strategy.Inner[3].Should().Match(s => IsUseWeaponStrategy(s, WeaponType.Primary));
            strategy.Inner[4].Should().Match(s => IsUseWeaponStrategy(s, WeaponType.Melee));
            strategy.Inner[5].Should().Match(s => IsUseWeaponStrategy(s, WeaponType.Secondary));
            strategy.Inner[6].Should().Match(s => IsUseWeaponStrategy(s, WeaponType.Temporary));
        }
    }

    private bool IsUseWeaponStrategy(object strategy, WeaponType weaponType)
    {
        return strategy is UseWeaponStrategy s 
            && s.Weapon == weaponType;
    }
}