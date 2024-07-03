namespace TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

public class EquippedWeapons
{
    public EquippedWeapons(WeaponContext? primary, WeaponContext? secondary, WeaponContext? melee, WeaponContext? temporary)
    {
        Primary = primary;
        Secondary = secondary;
        Melee = melee;
        Temporary = temporary;
    }

    public WeaponContext? Primary { get; }
    public WeaponContext? Secondary { get; }
    public WeaponContext? Melee { get; }
    public WeaponContext? Temporary { get; }
}