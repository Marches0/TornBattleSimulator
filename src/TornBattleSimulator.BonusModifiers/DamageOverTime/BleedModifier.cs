using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.BonusModifiers.DamageOverTime;

public class BleedModifier : IDamageOverTimeModifier
{
    /// <inheritdoc/>
    public double Decay => 0.45;

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Turns(9);

    /// <inheritdoc/>
    public bool RequiresDamageToApply => true;

    /// <inheritdoc/>
    public ModifierTarget Target => ModifierTarget.Other;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Bleed;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}