using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Build.Equipment;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;

[TestFixture]
public class ModifierFactoryTests
{
    [Test]
    public void GetModifier_ReturnsCorrectModifier([Values] ModifierType modifierType)
    {
        new ModifierFactory().GetModifier(modifierType, 50).Modifier.Effect.Should().Be(modifierType);
    }
}