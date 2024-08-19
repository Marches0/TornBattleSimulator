using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Options;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers.BodyParts;

public class BodyPartModifier : IDamageModifier
{
    private Dictionary<BodyPart, double> _damageMap;

    public BodyPartModifier(
        BodyModifierOptions bodyModifierOptions)
    {
        _damageMap = bodyModifierOptions.CriticalHits.Concat(bodyModifierOptions.RegularHits)
            .ToDictionary(d => d.Part, d => d.DamageMultiplier);
    }

    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(AttackContext attack, HitLocation hitLocation)
    {
        return _damageMap[hitLocation.BodyPartStruck];
    }
}