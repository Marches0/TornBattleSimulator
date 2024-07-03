namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class ReloadSecondaryAction : ReloadWeaponAction, IAction
{
    public void PerformAction(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        PerformAction(context, active, other, active.Secondary!);
    }
}