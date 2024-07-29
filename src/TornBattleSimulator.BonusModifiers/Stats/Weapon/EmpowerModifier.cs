using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Stats.Weapon;

public class EmpowerModifier : IStatsModifier, IModifier
{
    private readonly double _value;

    public EmpowerModifier(double value)
    {
        _value = 1 + value;
    }

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    /// <inheritdoc/>
    public bool RequiresDamageToApply => false;

    /// <inheritdoc/>
    public ModifierTarget Target => ModifierTarget.Self;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt => ModifierApplication.FightStart;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Empower;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public StatModificationType Type => StatModificationType.Additive;

    /// <inheritdoc/>
    public double GetDefenceModifier() => 1f;

    /// <inheritdoc/>
    public double GetDexterityModifier() => 1f;

    /// <inheritdoc/>
    public double GetSpeedModifier() => 1f;

    /// <inheritdoc/>
    public double GetStrengthModifier() => _value;
}