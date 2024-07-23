using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;
using TornBattleSimulator.Shared.Build;
using TornBattleSimulator.Shared.Build.Equipment;
using TornBattleSimulator.Shared.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy;

public class StrategyBuilder
{
    public IStrategy BuildStrategy(BattleBuild build)
    {
        // Stun is automatically the highest priority strategy, so
        // we don't act when it's applied.
        List<IStrategy> strategies = [new StunStrategy(), .. build.Strategy.Select(BuildStrategy)];
        return new CompositeStrategy(strategies);
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