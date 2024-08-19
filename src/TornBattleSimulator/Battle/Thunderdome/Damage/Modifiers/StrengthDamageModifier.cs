using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class StrengthDamageModifier : IDamageModifier
{
    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(AttackContext attack, HitLocation hitLocation)
    {
        //https://www.torn.com/forums.php#/p=threads&f=61&t=16199413&b=0&a=0
        double strength = attack.Active.Stats.Strength;
        double logStrength10 = Math.Log(strength / 10, 10);

        return (7d
            * Math.Pow(logStrength10, 2)
            + 27
            * logStrength10
            + 30)
            / 3.5;
    }
}