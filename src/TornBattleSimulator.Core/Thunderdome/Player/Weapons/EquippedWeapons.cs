namespace TornBattleSimulator.Core.Thunderdome.Player.Weapons;

public class EquippedWeapons : ITickable
{
    public EquippedWeapons(
        WeaponContext? primary,
        WeaponContext? secondary,
        WeaponContext? melee,
        WeaponContext? temporary
    )
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

    /// <inheritdoc/>
    public void FightBegin(ThunderdomeContext context)
    {
        Primary?.FightBegin(context);
        Secondary?.FightBegin(context);
        Melee?.FightBegin(context);
        Temporary?.FightBegin(context);
    }

    /// <inheritdoc/>
    public void OpponentActionComplete(ThunderdomeContext context)
    {
        Primary?.OpponentActionComplete(context);
        Secondary?.OpponentActionComplete(context);
        Melee?.OpponentActionComplete(context);
        Temporary?.OpponentActionComplete(context);
    }

    /// <inheritdoc/>
    public void OwnActionComplete(ThunderdomeContext context)
    {
        Primary?.OwnActionComplete(context);
        Secondary?.OwnActionComplete(context);
        Melee?.OwnActionComplete(context);
        Temporary?.OwnActionComplete(context);
    }

    /// <inheritdoc/>
    public void TurnComplete(ThunderdomeContext context)
    {
        Primary?.TurnComplete(context);
        Secondary?.TurnComplete(context);
        Melee?.TurnComplete(context);
        Temporary?.TurnComplete(context);
    }
}