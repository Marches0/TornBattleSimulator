using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Accuracy;

/// <summary>
///  A modifier which affects the target's accuracy.
/// </summary>
public interface IAccuracyModifier
{
    /// <summary>
    ///  Get the accuracy modifier.
    /// </summary>
    double GetAccuracyModifier(PlayerContext active, PlayerContext other, WeaponContext weapon);
}