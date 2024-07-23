using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Actions;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class MeleeWeaponStrategy : ChargeableWeaponStrategy, IStrategy
{
    private readonly StrategyDescription _strategyDescription;

    public MeleeWeaponStrategy(StrategyDescription strategyDescription)
    {
        _strategyDescription = strategyDescription;
    }

    public BattleAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other)
    {
        if (NeedsCharge(self.Weapons.Melee!))
        {
            return BattleAction.ChargeMelee;
        }

        return BattleAction.AttackMelee;
    }
}