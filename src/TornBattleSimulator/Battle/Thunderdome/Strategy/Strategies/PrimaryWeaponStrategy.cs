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
        return GetMove(context, self, other, self.Weapons.Primary!) switch
        {
            { } when Disarmed(self.Weapons.Primary!) => BattleAction.DisarmPrimary,
            { } when NeedsCharge(self.Weapons.Primary!) => BattleAction.ChargePrimary,
            LoadableWeaponAction.Attack => BattleAction.AttackPrimary,
            LoadableWeaponAction.Reload => BattleAction.ReloadPrimary,
            null => null,
            var x => throw new ArgumentOutOfRangeException($"{x} is not a valid value.")
        };
    }
}