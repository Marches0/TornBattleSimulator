using TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Player;

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