using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.BonusModifiers.Damage;

public class DoubleEdgedModifier : IModifier, IDamageModifier, IHealthModifier
{
    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterOwnAction);

    /// <inheritdoc/>
    public bool RequiresDamageToApply => false;

    /// <inheritdoc/>
    public ModifierTarget Target => ModifierTarget.Self;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt => ModifierApplication.BeforeAction;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.DoubleEdged;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;

    /// <inheritdoc/>
    public StatModificationType Type => StatModificationType.Multiplicative;

    /// <inheritdoc/>
    public bool AppliesOnActivation => false;

    /// <inheritdoc/>
    public DamageModifierResult GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext) => new(2);

    /// <inheritdoc/>
    public int GetHealthModifier(PlayerContext target, DamageResult? damage) => -(int)(damage!.Damage * 0.25);
}