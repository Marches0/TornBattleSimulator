using TornBattleSimulator.Shared.Extensions;
using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Actions;
using TornBattleSimulator.Shared.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Events.Data;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Action;

public class StunnedAction : IAction
{
    public List<ThunderdomeEvent> PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other)
    {
        return [ context.CreateEvent(active, ThunderdomeEventType.Stunned, new StunnedData()) ];
    }
}