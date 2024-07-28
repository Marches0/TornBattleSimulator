using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Ammo;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

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