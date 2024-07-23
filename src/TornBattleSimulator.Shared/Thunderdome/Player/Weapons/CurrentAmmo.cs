using TornBattleSimulator.Shared.Build.Equipment;

namespace TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

public class CurrentAmmo : Ammo
{
    public int MagazineAmmoRemaining { get; set; }
    public int MagazinesRemaining { get; set; }
}