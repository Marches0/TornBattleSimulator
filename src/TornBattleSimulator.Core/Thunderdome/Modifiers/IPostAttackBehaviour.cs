using TornBattleSimulator.Core.Thunderdome.Events;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

/// <summary>
///  Performs a custom behaviour after an attack has been made.
/// </summary>
public interface IPostAttackBehaviour
{
    List<ThunderdomeEvent> PerformAction(AttackContext attack, bool bonusAction);
}