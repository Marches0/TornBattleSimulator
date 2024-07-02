﻿using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class PlayerContextBuilder
{
    private BattleStats? _battleStats;
    private Weapon? _primary;
    private Weapon? _secondary;
    private Weapon? _melee;

    public PlayerContextBuilder WithStats(BattleStats battleStats)
    {
        _battleStats = battleStats;
        return this;
    }

    public PlayerContextBuilder WithPrimary(Weapon weapon)
    {
        _primary = weapon;
        return this;
    }

    public PlayerContextBuilder WithSecondary(Weapon weapon)
    {
        _secondary = weapon;
        return this;
    }

    public PlayerContextBuilder WithMelee(Weapon weapon)
    {
        _melee = weapon;
        return this;
    }

    public PlayerContext Build()
    {
        return new PlayerContext(
            new BattleBuild()
            {
                BattleStats = _battleStats,
                Primary = _primary,
                Secondary = _secondary,
                Melee = _melee
            }
        );
    }
}