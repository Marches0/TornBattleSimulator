using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

/// <summary>
///  A modifier that will never trigger, and has no effect.
/// </summary>
public class NoOpModifier : IModifier
{
    public NoOpModifier(ModifierType effect)
    {
        Effect = effect;
    }

    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Turns(0);

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => ModifierTarget.Self;

    public ModifierApplication AppliesAt => ModifierApplication.Never;

    public ModifierType Effect { get; }

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.None;
}