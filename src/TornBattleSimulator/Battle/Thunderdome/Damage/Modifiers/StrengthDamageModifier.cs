using TornBattleSimulator.Shared.Thunderdome.Damage;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class StrengthDamageModifier : IDamageModifier
{
    public DamageModifierResult GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext)
    {
        //https://www.torn.com/forums.php#/p=threads&f=61&t=16199413&b=0&a=0
        double strength = active.Stats.Strength;
        double logStrength10 = Math.Log(strength / 10, 10);

        var damage  = (7d
            * Math.Pow(logStrength10, 2)
            + 27
            * logStrength10
            + 30)
            / 3.5;

        return new DamageModifierResult(damage);
    }
}