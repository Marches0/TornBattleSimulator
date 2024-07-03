using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Description;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class SecondaryWeaponStrategy : LoadableWeaponStrategy, IStrategy
{
    public SecondaryWeaponStrategy(
        StrategyDescription strategyDescription) : base(strategyDescription)
    {
    }

    public BattleAction? GetMove(
        ThunderdomeContext context,
        PlayerContext self,
        PlayerContext other)
    {
        return GetMove(context, self, other, self.Secondary!) switch
        {
            LoadableWeaponAction.Attack => BattleAction.AttackSecondary,
            LoadableWeaponAction.Reload => BattleAction.ReloadSecondary,
            null => null,
            var x => throw new ArgumentOutOfRangeException($"{x} is not a valid value.")
        };
    }
}