using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Strategy;

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
        return GetMove(context, self, other, self.Weapons.Secondary!) switch
        {
            { } when NeedsCharge(self.Weapons.Secondary!) => BattleAction.ChargeSecondary,
            LoadableWeaponAction.Attack => BattleAction.AttackSecondary,
            LoadableWeaponAction.Reload => BattleAction.ReloadSecondary,
            null => null,
            var x => throw new ArgumentOutOfRangeException($"{x} is not a valid value.")
        };
    }
}