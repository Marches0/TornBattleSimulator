using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Accuracy;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestAccuracyModifier : IModifier, IAccuracyModifier
{
    private readonly double _accuracyModifier;

    public TestAccuracyModifier(double accuracyModifier)
    {
        _accuracyModifier = accuracyModifier;
    }

    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => ModifierTarget.Self;

    public ModifierApplication AppliesAt => ModifierApplication.BeforeAction;

    public ModifierType Effect => 0;

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

    public double GetAccuracyModifier(PlayerContext active, PlayerContext other, WeaponContext weapon)
    {
        return _accuracyModifier;
    }
}