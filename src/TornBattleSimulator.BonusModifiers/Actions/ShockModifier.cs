using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.BonusModifiers.Actions;

public class ShockModifier : IModifier
{
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterOwnAction);

    public bool RequiresDamageToApply { get; } = true;

    public ModifierTarget Target { get; } = ModifierTarget.Other;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    public ModifierType Effect { get; } = ModifierType.Shock;

    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Chance;
}