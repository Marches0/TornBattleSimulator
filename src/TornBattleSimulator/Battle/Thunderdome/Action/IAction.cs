using TornBattleSimulator.Battle.Thunderdome.Events;

namespace TornBattleSimulator.Battle.Thunderdome.Action;

public interface IAction
{
    List<ThunderdomeEvent> PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other);
}