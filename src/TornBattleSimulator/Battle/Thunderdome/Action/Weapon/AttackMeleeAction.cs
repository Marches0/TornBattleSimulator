using TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class AttackMeleeAction : IAction
{
    private readonly IWeaponUsage _weaponUsage;

    public AttackMeleeAction(
        IWeaponUsage weaponUsage)
    {
        _weaponUsage = weaponUsage;
    }

    public List<ThunderdomeEvent> PerformAction(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        return _weaponUsage.UseWeapon(context, active, other, active.Weapons.Melee!);
    }
}