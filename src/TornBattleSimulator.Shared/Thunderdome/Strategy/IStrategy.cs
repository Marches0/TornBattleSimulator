using TornBattleSimulator.Shared.Thunderdome.Actions;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Shared.Thunderdome.Strategy;

public interface IStrategy
{
    BattleAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other);
}