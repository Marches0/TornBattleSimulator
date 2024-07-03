using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Description;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class PrimaryWeaponStrategy : IStrategy
{
    private readonly StrategyDescription _strategyDescription;

    public PrimaryWeaponStrategy(
        StrategyDescription strategyDescription)
    {
        _strategyDescription = strategyDescription;
    }

    public BattleAction? GetMove(
        ThunderdomeContext context,
        PlayerContext self,
        PlayerContext other)
    {
        if (self.Primary!.RequiresReload)
        {
            if (!_strategyDescription.Reload)
            {
                return null;
            }

            if (!self.Primary.CanReload)
            {
                return null;
            }

            return BattleAction.ReloadPrimary;
        }

        return BattleAction.AttackPrimary;
    }
}