using TornBattleSimulator.Core.Build.Equipment;

namespace TornBattleSimulator.Core.Thunderdome.Player.Weapons;

public class CurrentAmmo : Ammo
{
    public int MagazineAmmoRemaining { get; set; }
    public int MagazinesRemaining { get; set; }
}