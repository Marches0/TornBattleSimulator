using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Events;

public class ThunderdomeEvent
{
    public ThunderdomeEvent(
        PlayerType source,
        ThunderdomeEventType type,
        int turn,
        IEventData data,
        int attackerHealth,
        int defenderHealth)
    {
        Source = source;
        Type = type;
        Turn = turn;
        Data = data;
        AttackerHealth = attackerHealth;
        DefenderHealth = defenderHealth;
    }

    public PlayerType Source { get; }
    public ThunderdomeEventType Type { get; }
    public int Turn { get; }
    public IEventData Data { get; }
    public int AttackerHealth { get; }
    public int DefenderHealth { get; }
}