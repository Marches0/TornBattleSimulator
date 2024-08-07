using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.BonusModifiers.Actions;

public class DisarmModifier : IModifier, IConditionalModifier
{
    public DisarmModifier(double value)
    {
        // Undo the /100 we do when converting this
        Lifespan = ModifierLifespanDescription.Turns((int)(value * 100));
    }

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; }

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = true;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.OtherWeapon;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Disarm;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public bool CanActivate(AttackContext attack) => 
           attack.AttackResult!.Damage!.BodyPart is BodyPart.Arms or BodyPart.Hands
        && attack.Other.ActiveWeapon != null;
}