using TornBattleSimulator.Core.Thunderdome.Events;

namespace TornBattleSimulator.Core.Thunderdome.Actions;

public interface IAction
{
    List<ThunderdomeEvent> PerformAction(AttackContext attack);
}