using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class StrengthDefenceRatioDamageModifier : IDamageModifier
{
    private static readonly double LowerMitigationModifier = 50 / Math.Log(32);
    private static readonly double UpperMitigationModifier = 50 / Math.Log(14);

    public DamageModifierResult GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        // https://www.torn.com/forums.php#/p=threads&f=61&t=16199413&b=0&a=0
        double defence = other.Stats.Defence;
        double strength = active.Stats.Strength;
        double ratio = defence / strength;

        double mitigation = ratio switch
        {
            <= 1 => LowerMitigation(ratio) / 100,
            _ => UpperMitigation(ratio) / 100
        };

        // Invert to apply to overall damage - 
        // a 10% mitigation = 90% damage 
        return new DamageModifierResult(1 - Math.Clamp(mitigation, 0, 1));
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