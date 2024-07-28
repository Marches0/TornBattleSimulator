using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Ammo;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestAmmoModifier : IModifier, IAmmoModifier
{
    private readonly double _value;

    public TestAmmoModifier(double value)
    {
        _value = value;
    }

    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => ModifierTarget.Self;

    public ModifierApplication AppliesAt => ModifierApplication.FightStart;

    public ModifierType Effect => 0;

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

    public double GetModifier() => _value;
}