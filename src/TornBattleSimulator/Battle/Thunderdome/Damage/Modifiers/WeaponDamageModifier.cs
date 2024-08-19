using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class WeaponDamageModifier : IDamageModifier
{
    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(AttackContext attack, HitLocation hitLocation)
    {
        return attack.Weapon.Description.Damage / 10;
    }
}