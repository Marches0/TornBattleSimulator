﻿using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;
using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy;

public class StrategyBuilder
{
    private readonly MissTurnStrategy _missTurn;

    public StrategyBuilder(MissTurnStrategy missTurn)
    {
        _missTurn = missTurn;
    }

    public IStrategy BuildStrategy(BattleBuild build)
    {
        // Turn Miss is automatically the highest priority strategy, so
        // we don't act when it's applied.
        List<IStrategy> strategies = [_missTurn, .. build.Strategy.Select(BuildStrategy)];
        return new CompositeStrategy(strategies);
    }

    private IStrategy BuildStrategy(StrategyDescription strategyDescription)
    {
        return strategyDescription.Weapon switch
        {
            WeaponType.Primary => new PrimaryWeaponStrategy(strategyDescription),
            WeaponType.Secondary => new SecondaryWeaponStrategy(strategyDescription),
            WeaponType.Melee => new MeleeWeaponStrategy(strategyDescription),
            WeaponType.Temporary => new TemporaryWeaponStrategy(strategyDescription),
            _ => throw new NotImplementedException($"Strategy {strategyDescription.Weapon}")
        };
    }
}