using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class PrimaryWeaponStrategy : LoadableWeaponStrategy, IStrategy
{
    public PrimaryWeaponStrategy(
        StrategyDescription strategyDescription) : base(strategyDescription)
    {

    }

    public BattleAction? GetMove(
        ThunderdomeContext context,
        PlayerContext self,
        PlayerContext other)
    {
        if (NeedsCharge(self.Weapons.Primary!))
        {
            return BattleAction.ChargePrimary;
        }

        return GetMove(context, self, other, self.Weapons.Primary!) switch
        {
            LoadableWeaponAction.Attack => BattleAction.AttackPrimary,
            LoadableWeaponAction.Reload => BattleAction.ReloadPrimary,
            null => null,
            var x => throw new ArgumentOutOfRangeException($"{x} is not a valid value.")
        };
    }
}