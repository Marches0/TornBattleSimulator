using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;

public interface IHealthModifierApplier
{
    ThunderdomeEvent ModifyHealth(
        ThunderdomeContext context,
        PlayerContext target,
        IHealthModifier healthModifier,
        AttackResult? attackResult);
}