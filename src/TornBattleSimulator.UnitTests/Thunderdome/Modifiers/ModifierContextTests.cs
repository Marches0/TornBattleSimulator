using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;

[TestFixture]
public class ModifierContextTests
{
    [Test]
    public void ModifierContext_AfterTick_RemovesExpiredModifiers()
    {
        // Arrange
        TestModifier expiringModifier = new TestModifier(ModifierLifespanDescription.Temporal(0.5f));
        TestModifier remainingModifier = new TestModifier(ModifierLifespanDescription.Temporal(1.5f));

        ModifierContext modifierContext = new();
        modifierContext.AddModifier(expiringModifier);
        modifierContext.AddModifier(remainingModifier);

        ThunderdomeContext thunderdomeContext = new ThunderdomeContextBuilder().WithParticipants(new PlayerContextBuilder(), new PlayerContextBuilder()).Build();

        // Act
        modifierContext.Tick(thunderdomeContext);

        // Assert
        using (new AssertionScope())
        {
            modifierContext.Active.Should().Contain(remainingModifier);
            modifierContext.Active.Should().NotContain(expiringModifier);
        }
    }
}