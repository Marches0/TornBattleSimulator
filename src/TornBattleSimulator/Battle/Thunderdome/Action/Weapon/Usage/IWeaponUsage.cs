using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Events;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

public interface IWeaponUsage
{
    public List<ThunderdomeEvent> UseWeapon(AttackContext attack);
}