using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Options;

public class BodyModifierOptions
{
    public List<BodyPartDamage> CriticalHits { get; set; } = new List<BodyPartDamage>();
    public List<BodyPartDamage> RegularHits { get; set; } = new List<BodyPartDamage>();
}

public class BodyPartDamage
{
    public BodyPart Part { get; set; }
    public float Chance { get; set; }
    public double DamageMultiplier { get; set; }
}