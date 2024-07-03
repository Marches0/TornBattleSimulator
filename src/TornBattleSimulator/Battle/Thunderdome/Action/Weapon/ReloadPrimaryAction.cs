namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class ReloadPrimaryAction : ReloadWeaponAction, IAction
{
    public void PerformAction(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        PerformAction(context, active, other, active.Primary!);
    }
}