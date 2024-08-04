using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;

public interface IModifierApplier
{
    List<ThunderdomeEvent> ApplyModifier(
        IModifier modifier,
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        AttackResult? attackResult);

    List<ThunderdomeEvent> ApplyOtherHeals(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        AttackResult? attackResult);
}