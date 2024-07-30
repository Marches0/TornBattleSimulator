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
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.Self;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.FightStart;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Empower;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public StatModificationType Type { get; } = StatModificationType.Additive;

    /// <inheritdoc/>
    public double GetDefenceModifier() => 1f;

    /// <inheritdoc/>
    public double GetDexterityModifier() => 1f;

    /// <inheritdoc/>
    public double GetSpeedModifier() => 1f;

    /// <inheritdoc/>
    public double GetStrengthModifier() => _value;
}