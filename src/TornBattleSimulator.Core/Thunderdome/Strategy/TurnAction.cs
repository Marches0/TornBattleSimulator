using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome.Strategy;

public class TurnAction
{
    public TurnAction(
        BattleAction? action,
        WeaponContext? weapon)
    {
        Action = action;
        Weapon = weapon;
    }

    public BattleAction? Action { get; }

    public WeaponContext? Weapon { get; }
}