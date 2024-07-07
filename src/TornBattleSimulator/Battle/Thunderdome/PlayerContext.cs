﻿using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Battle.Thunderdome.Player;
using TornBattleSimulator.Battle.Thunderdome.Player.Armours;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

namespace TornBattleSimulator.Battle.Thunderdome;

/// <summary>
///  A build taking part in a battle.
/// </summary>
public class PlayerContext
{
    public PlayerContext(
        BattleBuild build,
        PlayerType playerType,
        EquippedWeapons weapons,
        ArmourSetContext armourSet,
        IStrategy strategy)
    {
        Build = build;
        Strategy = strategy;
        Weapons = weapons;
        ArmourSet = armourSet;
        Health = new((int)build.Health);
        PlayerType = playerType;

        _currentTickStats = new Lazy<BattleStats>(GetCurrentStats);
    }

    /// <summary>
    ///  The build taking part.
    /// </summary>
    public BattleBuild Build { get; }

    public IStrategy Strategy { get; }

    /// <summary>
    ///  Modifiers currently applied to the player.
    /// </summary>
    public ModifierContext Modifiers { get; } = new();

    public PlayerHealth Health { get; set; }

    public EquippedWeapons Weapons { get; }
    public ArmourSetContext ArmourSet { get; }
    public PlayerType PlayerType { get; }

    /// <summary>
    ///  The build's current stats.
    /// </summary>
    public BattleStats Stats => _currentTickStats.Value;

    /// <summary>
    ///  The action being taken by this player in the current tick.
    /// </summary>
    public BattleAction CurrentAction { get; set; }

    public void Tick(ThunderdomeContext context)
    {
        // Clear the stats every tick, so we can reevaluate modifiers.
        _currentTickStats = new Lazy<BattleStats>(GetCurrentStats);
        CurrentAction = 0;

        Modifiers.Tick(context);
    }

    private Lazy<BattleStats> _currentTickStats;

    private BattleStats GetCurrentStats()
    {
        BattleStats baseStats = new()
        {
            Strength = Build.BattleStats.Strength,
            Defence = Build.BattleStats.Defence,
            Speed = Build.BattleStats.Speed,
            Dexterity = Build.BattleStats.Dexterity,
        };

        return Modifiers.Active
            .OfType<IStatsModifier>()
            .Aggregate(baseStats, (stats, modifier) => stats.Apply(modifier));
    }
}