using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.BonusModifiers.Actions;

public class ShockModifier : IModifier
{
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterOwnAction);

    public bool RequiresDamageToApply => true;

    public ModifierTarget Target => ModifierTarget.Other;

    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Shock;
}