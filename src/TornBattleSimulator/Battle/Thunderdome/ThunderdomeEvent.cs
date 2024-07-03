using TornBattleSimulator.Battle.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome;

public class ThunderdomeEvent
{
    public ThunderdomeEvent(
        PlayerType source,
        ThunderdomeEventType type,
        int turn,
        List<object> data)
    {
        Source = source;
        Type = type;
        Turn = turn;
        Data = data;
    }

    public PlayerType Source { get; }
    public ThunderdomeEventType Type { get; }
    public int Turn { get; }
    public List<object> Data { get; }
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