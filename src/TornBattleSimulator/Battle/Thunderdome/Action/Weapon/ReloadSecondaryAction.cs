namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Player;

public class ReloadSecondaryAction : ReloadWeaponAction, IAction
{
    public List<ThunderdomeEvent> PerformAction(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        return PerformAction(context, active, other, active.Weapons.Secondary!);
    }
}