using TornBattleSimulator.BonusModifiers.Ammo;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class MeleeWeaponStrategy : ChargeableWeaponStrategy, IStrategy
{
    private readonly StrategyDescription _strategyDescription;

    public MeleeWeaponStrategy(StrategyDescription strategyDescription)
    {
        _strategyDescription = strategyDescription;
    }

    public TurnAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other)
    {
        WeaponContext weapon = self.Weapons.Melee!;

        if (Disarmed(weapon))
        {
            return new(BattleAction.Disarmed, self.Weapons.Melee);
        }

        if (NeedsCharge(weapon))
        {
            return new(BattleAction.Charge, self.Weapons.Melee);
        }

        if (weapon.Modifiers.Active.OfType<StorageModifier>().Any())
        {
            return new(BattleAction.ReplenishTemporary, self.Weapons.Melee);
        }

        return new(BattleAction.Attack, self.Weapons.Melee);
    }
}