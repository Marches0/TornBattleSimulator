using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Description;
using TornBattleSimulator.Shared.Thunderdome.Player;

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
        if (NeedsCharge(self.Weapons.Secondary!))
        {
            return BattleAction.ChargeSecondary;
        }

        return GetMove(context, self, other, self.Weapons.Secondary!) switch
        {
            LoadableWeaponAction.Attack => BattleAction.AttackSecondary,
            LoadableWeaponAction.Reload => BattleAction.ReloadSecondary,
            null => null,
            var x => throw new ArgumentOutOfRangeException($"{x} is not a valid value.")
        };
    }
}