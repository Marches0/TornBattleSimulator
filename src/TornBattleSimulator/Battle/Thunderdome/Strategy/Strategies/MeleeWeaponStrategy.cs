using TornBattleSimulator.BonusModifiers.Ammo;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Strategy;

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
        if (Disarmed(self.Weapons.Melee!))
        {
            return BattleAction.DisarmMelee;
        }

        if (NeedsCharge(self.Weapons.Melee!))
        {
            return BattleAction.ChargeMelee;
        }

        if (self.Weapons.Melee!.Modifiers.Active.OfType<StorageModifier>().Any())
        {
            return BattleAction.ReplenishTemporary;
        }

        return BattleAction.AttackMelee;
    }
}