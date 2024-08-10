using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class StrengthDefenceRatioDamageModifier : IDamageModifier
{
    private static readonly double LowerMitigationModifier = 50 / Math.Log(32);
    private static readonly double UpperMitigationModifier = 50 / Math.Log(14);

    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(AttackContext attack, HitLocation hitLocation)
    {
        // https://www.torn.com/forums.php#/p=threads&f=61&t=16199413&b=0&a=0
        double defence = attack.Other.Stats.Defence;
        double strength = attack.Active.Stats.Strength;
        double ratio = defence / strength;

        double mitigation = ratio switch
        {
            <= 1 => LowerMitigation(ratio) / 100,
            _ => UpperMitigation(ratio) / 100
        };

        // Invert to apply to overall damage - 
        // a 10% mitigation = 90% damage 
        return 1 - Math.Clamp(mitigation, 0, 1);
    }

    private double LowerMitigation(double ratio)
    {
        return LowerMitigationModifier * Math.Log(ratio) + 50;
    }

    private double UpperMitigation(double ratio)
    {
        return UpperMitigationModifier * Math.Log(ratio) + 50;
    }
}