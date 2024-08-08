using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class TemporaryWeaponStrategy : IStrategy
{
    private readonly StrategyDescription _strategyDescription;

    public TemporaryWeaponStrategy(StrategyDescription strategyDescription)
    {
        _strategyDescription = strategyDescription;
    }

    public TurnAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other)
    {
        return self.Weapons.Temporary != null && self.Weapons.Temporary.Ammo!.MagazineAmmoRemaining > 0
            ? new TurnAction(BattleAction.Attack, self.Weapons.Temporary)
            : null;
    }
}