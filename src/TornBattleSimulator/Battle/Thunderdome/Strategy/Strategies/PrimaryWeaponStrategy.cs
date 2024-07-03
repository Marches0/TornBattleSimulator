﻿using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Description;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class PrimaryWeaponStrategy : LoadableWeaponStrategy, IStrategy
{
    public PrimaryWeaponStrategy(
        StrategyDescription strategyDescription) : base(strategyDescription)
    {

    }

    public BattleAction? GetMove(
        ThunderdomeContext context,
        PlayerContext self,
        PlayerContext other)
    {
        return GetMove(context, self, other, self.Primary!) switch
        {
            LoadableWeaponAction.Attack => BattleAction.AttackPrimary,
            LoadableWeaponAction.Reload => BattleAction.ReloadPrimary,
            null => null,
            var x => throw new ArgumentOutOfRangeException($"{x} is not a valid value.")
        };
    }
}