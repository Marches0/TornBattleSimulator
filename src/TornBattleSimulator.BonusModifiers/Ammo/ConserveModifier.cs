using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Ammo;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.BonusModifiers.Ammo;

public class ConserveModifier : IModifier, IAmmoModifier
{
    private readonly double _value;

    public ConserveModifier(double value)
    {
        _value = value;
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
    public ModifierType Effect => ModifierType.Conserve;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public double GetModifier() => _value;
}