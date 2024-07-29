using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.BonusModifiers.Attacks;

public class DoubleTapModifier : IAttacksModifier
{
    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterOwnAction);

    /// <inheritdoc/>
    public bool RequiresDamageToApply => true;

    /// <inheritdoc/>
    public ModifierTarget Target => ModifierTarget.Self;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.DoubleTap;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}