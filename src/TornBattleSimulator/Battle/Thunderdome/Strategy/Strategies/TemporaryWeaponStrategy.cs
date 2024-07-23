using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Actions;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class TemporaryWeaponStrategy : IStrategy
{
    private readonly StrategyDescription _strategyDescription;

    public TemporaryWeaponStrategy(StrategyDescription strategyDescription)
    {
        _strategyDescription = strategyDescription;
    }

    public BattleAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other)
    {
        return self.Weapons.Temporary != null && self.Weapons.Temporary.Ammo!.MagazineAmmoRemaining > 0
            ? BattleAction.UseTemporary
            : null;
    }
}