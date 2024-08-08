using System;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class DisarmedAction : IAction
{
    public List<ThunderdomeEvent> PerformAction(AttackContext attack)
    {
        return [attack.Context.CreateEvent(attack.Active, ThunderdomeEventType.Disarmed, new DisarmedData(attack.Weapon.Type))];
    }
}