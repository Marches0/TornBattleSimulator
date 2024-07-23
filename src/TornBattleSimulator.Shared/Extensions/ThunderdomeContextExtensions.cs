using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Core.Extensions;

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