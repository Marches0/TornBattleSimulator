using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;
using TornBattleSimulator.Shared.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public abstract class LoadableWeaponStrategy : ChargeableWeaponStrategy
{
    private readonly StrategyDescription _strategyDescription;

    protected LoadableWeaponStrategy(StrategyDescription _strategyDescription)
    {
        this._strategyDescription = _strategyDescription;
    }

    protected LoadableWeaponAction? GetMove(
        ThunderdomeContext context,
        PlayerContext self,
        PlayerContext other,
        WeaponContext weapon)
    {
        if (weapon!.RequiresReload)
        {
            if (!_strategyDescription.Reload || !weapon.CanReload)
            {
                return null;
            }

            return LoadableWeaponAction.Reload;
        }

        return LoadableWeaponAction.Attack;
    }

    protected enum LoadableWeaponAction
    {
        Attack = 1,
        Reload = 2
    }
}