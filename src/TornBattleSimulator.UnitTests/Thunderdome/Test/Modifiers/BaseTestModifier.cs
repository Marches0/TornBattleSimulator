using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public abstract class BaseTestModifier : IModifier
{
    public virtual ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    public virtual bool RequiresDamageToApply { get; } = false;

    public virtual ModifierTarget Target { get; } = ModifierTarget.Self;

    public virtual ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    public virtual ModifierType Effect { get; } = 0;

    public virtual ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;
}