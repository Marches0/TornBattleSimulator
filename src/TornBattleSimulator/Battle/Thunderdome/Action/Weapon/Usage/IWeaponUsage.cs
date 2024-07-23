using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

public interface IWeaponUsage
{
    public List<ThunderdomeEvent> UseWeapon(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon);
}