using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

/// <summary>
///  Performs a custom behaviour after an attack has been made.
/// </summary>
public interface IPostAttackBehaviour
{
    List<ThunderdomeEvent> PerformAction(ThunderdomeContext context, PlayerContext active, PlayerContext other, WeaponContext weapon, AttackResult attackResult, bool bonusAction);
}