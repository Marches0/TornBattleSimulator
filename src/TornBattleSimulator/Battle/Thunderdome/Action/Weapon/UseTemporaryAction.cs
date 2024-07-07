using TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;
using TornBattleSimulator.Battle.Thunderdome.Events;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class UseTemporaryAction : IAction
{
    private readonly IWeaponUsage _weaponUsage;

    public UseTemporaryAction(
        IWeaponUsage weaponUsage)
    {
        _weaponUsage = weaponUsage;
    }

    public List<ThunderdomeEvent> PerformAction(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        return _weaponUsage.UseWeapon(context, active, other, active.Weapons.Temporary!);
    }
}