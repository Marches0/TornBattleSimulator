using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class WeaponDamageModifier : IDamageModifier
{
    public double GetDamageModifier(
        PlayerContext attacker,
        PlayerContext defender)
    {
        double weaponDamage = attacker.CurrentAction switch
        {
            BattleAction.AttackPrimary => attacker.Build.Primary.Damage,
            BattleAction.AttackSecondary => attacker.Build.Secondary.Damage,
            BattleAction.AttackMelee => attacker.Build.Melee.Damage,
            _ => throw new ArgumentOutOfRangeException($"Cannot attack in a {attacker.CurrentAction} action.")
        };

        return weaponDamage / 10;
    }
}