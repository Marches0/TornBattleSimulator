using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Events.Data;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Shared.Extensions;

public static class ThunderdomeContextExtensions
{
    public static ThunderdomeEvent CreateEvent(
        this ThunderdomeContext ctx,
        PlayerContext? source,
        ThunderdomeEventType type,
        IEventData data)
    {
        return new ThunderdomeEvent(
            source?.PlayerType ?? 0,
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