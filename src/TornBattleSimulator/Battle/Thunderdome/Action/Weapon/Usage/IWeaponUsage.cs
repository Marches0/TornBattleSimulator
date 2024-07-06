using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

public interface IWeaponUsage
{
    public List<ThunderdomeEvent> UseWeapon(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon);
}