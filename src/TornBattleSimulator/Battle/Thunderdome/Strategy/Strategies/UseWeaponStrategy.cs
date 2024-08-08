using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Strategy;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.BonusModifiers.Ammo;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class UseWeaponStrategy : IStrategy
{
    private readonly StrategyDescription _strategyDescription;
    private readonly IUntilConditionResolver _untilConditionResolver;

    public WeaponType Weapon => _strategyDescription.Weapon;

    public UseWeaponStrategy(
        StrategyDescription strategyDescription,
        IUntilConditionResolver untilConditionResolver)
    {
        _strategyDescription = strategyDescription;
        _untilConditionResolver = untilConditionResolver;
    }

    public TurnAction? GetMove(
        ThunderdomeContext context,
        PlayerContext self,
        PlayerContext other)
    {
        WeaponContext? weapon = GetWeapon(self);
        if (weapon == null)
        {
            return null;
        }

        if (_untilConditionResolver.Fulfilled(
            new AttackContext(context, self, other, weapon, null), _strategyDescription))
        {
            return null;
        }

        BattleAction? desiredAction = GetDesiredAction(weapon);
        if (desiredAction == null)
        {
            return null;
        }

        if (weapon.Modifiers.Active.OfType<DisarmModifier>().Any())
        {
            return new TurnAction(BattleAction.Disarmed, weapon);
        }

        if (weapon.Modifiers.ChargeModifiers.Any(c => !c.Charged))
        {
            return new TurnAction(BattleAction.Charge, weapon);
        }

        if (weapon.Modifiers.Active.OfType<StorageModifier>().Any())
        {
            return new(BattleAction.ReplenishTemporary, weapon);
        }

        return new TurnAction(desiredAction, weapon);
    }

    private BattleAction? GetDesiredAction(WeaponContext weapon)
    {
        if (weapon!.RequiresReload)
        {
            if (!_strategyDescription.Reload || !weapon.CanReload)
            {
                return null;
            }

            return BattleAction.Reload;
        }

        return BattleAction.Attack;
    }

    private WeaponContext? GetWeapon(PlayerContext self)
    {
        return _strategyDescription.Weapon switch
        {
            WeaponType.Primary => self.Weapons.Primary,
            WeaponType.Secondary => self.Weapons.Secondary,
            WeaponType.Melee => self.Weapons.Melee,
            WeaponType.Temporary => self.Weapons.Temporary,
        };
    }
}