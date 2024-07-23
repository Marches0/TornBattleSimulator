using TornBattleSimulator.Shared.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Shared.Thunderdome.Modifiers.Attacks;

public interface IAttacksModifier : IModifier
{
    List<ThunderdomeEvent> MakeAttack(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        Func<List<ThunderdomeEvent>> attackAction);
}