using TornBattleSimulator.Shared.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Shared.Thunderdome.Actions;

public interface IAction
{
    List<ThunderdomeEvent> PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other);
}