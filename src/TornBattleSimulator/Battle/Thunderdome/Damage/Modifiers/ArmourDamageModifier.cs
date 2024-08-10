using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.BonusModifiers.Armour;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class ArmourDamageModifier : IDamageModifier
{
    private readonly IChanceSource _chanceSource;

    public ArmourDamageModifier(
        IChanceSource chanceSource)
    {
        _chanceSource = chanceSource;
    }

    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(AttackContext attack, HitLocation hitLocation)
    {
        if (hitLocation.ArmourStruck == null || attack.Other.Modifiers.Active.OfType<PunctureModifier>().Any())
        {
            return 1;
        }

        double penetrationReduction = attack.Weapon.Modifiers.Active
            .OfType<PenetrateModifier>()
            .Aggregate(1d, (total, mod) => total *= mod.ArmourRemaining);

        return 1d - (hitLocation.ArmourStruck.Rating * penetrationReduction);
    }
}