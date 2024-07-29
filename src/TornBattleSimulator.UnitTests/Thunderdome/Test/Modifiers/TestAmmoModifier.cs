using TornBattleSimulator.Core.Thunderdome.Modifiers.Ammo;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestAmmoModifier : BaseTestModifier, IAmmoModifier
{
    private readonly double _value;

    public TestAmmoModifier(double value)
    {
        _value = value;
    }

    public double GetModifier() => _value;
}