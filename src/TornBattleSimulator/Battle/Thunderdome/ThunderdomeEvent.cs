﻿using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Battle.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome;

public class ThunderdomeEvent
{
    public ThunderdomeEvent(
        PlayerType source,
        ThunderdomeEventType type,
        int turn,
        IEventData data)
    {
        Source = source;
        Type = type;
        Turn = turn;
        Data = data;
    }

    public PlayerType Source { get; }
    public ThunderdomeEventType Type { get; }
    public int Turn { get; }
    public IEventData Data { get; }
}

public enum ThunderdomeEventType
{
    AttackHit = 1,
    AttackMiss,
    Reload,
    Stunned,
    EffectBegin,
    EffectEnd
}