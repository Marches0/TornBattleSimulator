using TornBattleSimulator.Battle.Thunderdome.Events;
using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Extensions;

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