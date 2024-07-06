using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Description;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy;

public class StrategyBuilder
{
    public IStrategy BuildStrategy(BattleBuild build)
    {
        return new CompositeStrategy(build.Strategy.Select(BuildStrategy).ToList());
    }

    private IStrategy BuildStrategy(StrategyDescription strategyDescription)
    {
        return strategyDescription.Weapon switch
        {
            WeaponType.Primary => new PrimaryWeaponStrategy(strategyDescription),
            WeaponType.Secondary => new SecondaryWeaponStrategy(strategyDescription),
            WeaponType.Melee => new MeleeWeaponStrategy(strategyDescription),
            WeaponType.Temporary => new TemporaryWeaponStrategy(strategyDescription),
            _ => throw new NotImplementedException($"Strategy {strategyDescription.Weapon}")
        };
    }
}