using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;

public class DamageModifierResult
{
    public DamageModifierResult(double multiplier) : this(multiplier, 0)
    {

    }

    public DamageModifierResult(double multiplier, BodyPart bodyPart)
    {
        Multiplier = multiplier;
        BodyPart = bodyPart;
    }

    public double Multiplier { get; set; }

    public BodyPart BodyPart { get; set; }
}