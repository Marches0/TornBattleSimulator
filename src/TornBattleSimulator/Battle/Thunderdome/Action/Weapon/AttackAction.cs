using TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class AttackAction : IAction
{
    private readonly IWeaponUsage _weaponUsage;

    public AttackAction(
        IWeaponUsage weaponUsage)
    {
        _weaponUsage = weaponUsage;
    }

    public List<ThunderdomeEvent> PerformAction(AttackContext attack)
    {
        return _weaponUsage.UseWeapon(attack);
    }
}