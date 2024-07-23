using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Description;
using TornBattleSimulator.Shared.Thunderdome.Player;

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