using TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;
using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Actions;
using TornBattleSimulator.Shared.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class AttackPrimaryAction : IAction
{
    private readonly IWeaponUsage _weaponUsage;

    public AttackPrimaryAction(
        IWeaponUsage weaponUsage)
    {
        _weaponUsage = weaponUsage;
    }

    public List<ThunderdomeEvent> PerformAction(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        return _weaponUsage.UseWeapon(context, active, other, active.Weapons.Primary!);
    }
}