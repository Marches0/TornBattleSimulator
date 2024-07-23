using TornBattleSimulator.Shared.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Shared.Thunderdome.Damage;

public class DamageResult
{
    public DamageResult(
        int damage,
        BodyPart bodyPart,
        DamageFlags flags)
    {
        Damage = damage;
        BodyPart = bodyPart;
        Flags = flags;
    }

    public int Damage { get; }

    public BodyPart BodyPart { get; }

    public DamageFlags Flags { get; }
}