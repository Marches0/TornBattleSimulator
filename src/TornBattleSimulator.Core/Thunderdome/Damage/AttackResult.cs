using System.Diagnostics.CodeAnalysis;

namespace TornBattleSimulator.Core.Thunderdome.Damage;

public class AttackResult
{
    public AttackResult(
        bool hit,
        double hitChance,
        DamageResult? damage)
    {
        if (hit && damage == null)
        {
            throw new ArgumentException("Cannot have a hit with null damage");
        }

        Hit = hit;
        HitChance = hitChance;
        Damage = damage;
    }

    public bool Hit { get; set; }

    public double HitChance { get; set; }

    [MemberNotNullWhen(true, nameof(Hit))]
    public DamageResult? Damage { get; set; }
}