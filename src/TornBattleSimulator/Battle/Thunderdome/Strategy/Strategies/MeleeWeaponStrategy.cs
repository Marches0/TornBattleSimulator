using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Description;

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