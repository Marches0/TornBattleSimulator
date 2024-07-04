namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class ReloadSecondaryAction : ReloadWeaponAction, IAction
{
    public List<ThunderdomeEvent> PerformAction(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        return PerformAction(context, active, other, active.Weapons.Secondary!);
    }
}