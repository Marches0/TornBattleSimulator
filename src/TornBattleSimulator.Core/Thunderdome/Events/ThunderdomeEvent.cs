﻿using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome.Events;

public class ThunderdomeEvent
{
    public ThunderdomeEvent(
        PlayerType source,
        ThunderdomeEventType type,
        int turn,
        IEventData data,
        int attackerHealth,
        int defenderHealth,
        BattleStats attackerStats,
        BattleStats defenderStats)
    {
        Source = source;
        Type = type;
        Turn = turn;
        Data = data;
        AttackerHealth = attackerHealth;
        DefenderHealth = defenderHealth;
        AttackerStats = attackerStats;
        DefenderStats = defenderStats;
    }

    public PlayerType Source { get; }
    public ThunderdomeEventType Type { get; }
    public int Turn { get; }
    public IEventData Data { get; }
    public int AttackerHealth { get; }
    public int DefenderHealth { get; }
    public BattleStats AttackerStats { get; }
    public BattleStats DefenderStats { get; }
}