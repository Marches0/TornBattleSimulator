using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Extensions;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class DisarmedSecondaryAction : IAction
{
    public List<ThunderdomeEvent> PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other)
    {
        return [context.CreateEvent(active, ThunderdomeEventType.Disarmed, new DisarmedData(WeaponType.Secondary))];
    }
}