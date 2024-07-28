namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Ammo;

/// <summary>
///  A modifier against ammo consumption.
/// </summary>
public interface IAmmoModifier
{
    /// <summary>
    ///  Get the modifier.
    /// </summary>
    double GetModifier();
}