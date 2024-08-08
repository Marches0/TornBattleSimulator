using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class SecondaryWeaponStrategy : LoadableWeaponStrategy, IStrategy
{
    public SecondaryWeaponStrategy(
        StrategyDescription strategyDescription) : base(strategyDescription)
    {
    }

    public TurnAction? GetMove(
        ThunderdomeContext context,
        PlayerContext self,
        PlayerContext other)
    {
        WeaponContext weapon = self.Weapons.Secondary!;

        BattleAction? action = GetMove(context, self, other, weapon) switch
        {
            { } when Disarmed(weapon) => BattleAction.Disarmed,
            { } when NeedsCharge(weapon) => BattleAction.Charge,
            LoadableWeaponAction.Attack => BattleAction.Attack,
            LoadableWeaponAction.Reload => BattleAction.Reload,
            null => null,
            var x => throw new ArgumentOutOfRangeException($"{x} is not a valid value.")
        };

        return action != null
            ? new TurnAction(action.Value, weapon)
            : null;
    }
}