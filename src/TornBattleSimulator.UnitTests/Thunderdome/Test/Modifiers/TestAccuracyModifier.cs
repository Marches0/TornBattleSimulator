using TornBattleSimulator.Core.Thunderdome.Modifiers.Accuracy;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestAccuracyModifier : BaseTestModifier, IAccuracyModifier
{
    private readonly double _accuracyModifier;

    public TestAccuracyModifier(double accuracyModifier)
    {
        _accuracyModifier = accuracyModifier;
    }

    public double GetAccuracyModifier(PlayerContext active, PlayerContext other, WeaponContext weapon)
    {
        return _accuracyModifier;
    }
}