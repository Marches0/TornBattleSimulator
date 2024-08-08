using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Actions;

namespace TornBattleSimulator.Core.Thunderdome.Strategy;

public class TurnActionHistory
{
    public TurnActionHistory(BattleAction action, WeaponType weapon)
    {
        Action = action;
        Weapon = weapon;
    }

    public BattleAction Action { get; }
    public WeaponType Weapon { get; }
}