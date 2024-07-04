using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;
public class TestModifier : IModifier
{
    public TestModifier(ModifierLifespanDescription lifespan)
    {
        Lifespan = lifespan;
    }

    public ModifierLifespanDescription Lifespan { get; }

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => ModifierTarget.Self;

    public ModifierApplication AppliesAt => ModifierApplication.BeforeAction;

    public WeaponModifierType Effect => WeaponModifierType.Gassed;
}