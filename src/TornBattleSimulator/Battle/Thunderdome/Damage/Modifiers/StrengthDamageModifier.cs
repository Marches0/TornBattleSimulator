namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class StrengthDamageModifier : IDamageModifier
{
    public double GetDamageModifier(
        PlayerContext attacker,
        PlayerContext defender)
    {
        //https://www.torn.com/forums.php#/p=threads&f=61&t=16199413&b=0&a=0
        double strength = attacker.Stats.Strength;
        double logStrength10 = Math.Log(strength / 10, 10);

        return 7d
            * Math.Pow(logStrength10, 2)
            + 27
            * logStrength10
            + 30;
    }
}