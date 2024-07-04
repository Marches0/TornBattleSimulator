namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class ReloadPrimaryAction : ReloadWeaponAction, IAction
{
    public List<ThunderdomeEvent> PerformAction(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        return PerformAction(context, active, other, active.Weapons.Primary!);
    }
}