using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Damage;

public readonly struct DamageResult
{
    public DamageResult(
        int damage,
        BodyPart bodyPart)
    {
        Damage = damage;
        BodyPart = bodyPart;
    }

    public int Damage { get; }

    public BodyPart BodyPart { get; }
}