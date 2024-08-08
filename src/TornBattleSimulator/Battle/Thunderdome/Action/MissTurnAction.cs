using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Action;

public class MissTurnAction : IAction
{
    public List<ThunderdomeEvent> PerformAction(AttackContext attack)
    {
        return [ attack.Context.CreateEvent(attack.Active, ThunderdomeEventType.MissedTurn, new MissedTurn()) ];
    }
}