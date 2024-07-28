using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public abstract class BaseTestModifier : IModifier
{
    public virtual ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    public virtual bool RequiresDamageToApply => false;

    public virtual ModifierTarget Target => ModifierTarget.Self;

    public virtual ModifierApplication AppliesAt => ModifierApplication.FightStart;

    public virtual ModifierType Effect => 0;

    public virtual ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;
}