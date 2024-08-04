using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Core.Thunderdome.Damage;

public class DamageResult
{
    public DamageResult(
        int damage,
        BodyPart bodyPart,
        DamageFlags flags)
    {
        DamageDealt = damage;
        BodyPart = bodyPart;
        Flags = flags;
    }

    public int DamageDealt { get; }

    public BodyPart BodyPart { get; }

    public DamageFlags Flags { get; }
}