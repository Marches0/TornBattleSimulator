using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.BonusModifiers.Health;

public class BloodlustModifier : IHealthModifier
{
    private readonly double _value;

    public BloodlustModifier(double value)
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
    public ModifierType Effect => ModifierType.Bloodlust;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public bool AppliesOnActivation => false;

    /// <inheritdoc/>
    public int GetHealthModifier(PlayerContext target, DamageResult? damage) => (int)(damage!.Damage * _value);
}