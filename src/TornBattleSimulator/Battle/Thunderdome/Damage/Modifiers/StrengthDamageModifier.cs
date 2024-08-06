using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class StrengthDamageModifier : IDamageModifier
{
    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext)
    {
        //https://www.torn.com/forums.php#/p=threads&f=61&t=16199413&b=0&a=0
        double strength = active.Stats.Strength;
        double logStrength10 = Math.Log(strength / 10, 10);

        return (7d
            * Math.Pow(logStrength10, 2)
            + 27
            * logStrength10
            + 30)
            / 3.5;
    }
}