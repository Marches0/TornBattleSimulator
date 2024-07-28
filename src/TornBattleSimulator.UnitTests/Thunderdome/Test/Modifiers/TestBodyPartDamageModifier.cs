using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage.BodyParts;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestBodyPartDamageModifier : BodyPartDamageModifier, IModifier
{
    public TestBodyPartDamageModifier(BodyPart bodyPart, double damage) : base(bodyPart, damage)
    {
    }

    public ModifierType Effect => 0;
}