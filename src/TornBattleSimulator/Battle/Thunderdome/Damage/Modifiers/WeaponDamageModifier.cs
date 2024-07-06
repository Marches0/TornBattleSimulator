using TornBattleSimulator.Battle.Thunderdome.Action;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class WeaponDamageModifier : IDamageModifier
{
    public double GetDamageModifier(
        PlayerContext attacker,
        PlayerContext defender)
    {
        double weaponDamage = attacker.CurrentAction switch
        {
            BattleAction.AttackPrimary => attacker.Weapons.Primary!.Description.Damage,
            BattleAction.AttackSecondary => attacker.Weapons.Secondary!.Description.Damage,
            BattleAction.AttackMelee => attacker.Weapons.Melee!.Description.Damage,
            BattleAction.UseTemporary => attacker.Weapons.Temporary!.Description.Damage,
            _ => throw new ArgumentOutOfRangeException($"Cannot attack in a {attacker.CurrentAction} action.")
        };

        return weaponDamage / 10;
    }
}