using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Events;
using TornBattleSimulator.Battle.Thunderdome.Events.Data;

namespace TornBattleSimulator.Extensions;

public static class ThunderdomeContextExtensions
{
    public static ThunderdomeEvent CreateEvent(
        this ThunderdomeContext ctx,
        PlayerContext source,
        ThunderdomeEventType type,
        IEventData data)
    {
        return new ThunderdomeEvent(
            source.PlayerType,
            type,
            ctx.Turn,
            data,
            ctx.Attacker.Health.CurrentHealth,
            ctx.Defender.Health.CurrentHealth,
            ctx.Attacker.Stats.Copy(),
            ctx.Defender.Stats.Copy()
        );
    }
}