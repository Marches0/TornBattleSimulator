using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Damage;
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
        modifierContext.AddModifier(expiringModifier, null);
        modifierContext.AddModifier(remainingModifier, null);

        ThunderdomeContext thunderdomeContext = new ThunderdomeContextBuilder().WithParticipants(new PlayerContextBuilder(), new PlayerContextBuilder()).Build();

        // Act
        modifierContext.Tick(thunderdomeContext, new PlayerContextBuilder().Build());

        // Assert
        using (new AssertionScope())
        {
            modifierContext.Active.Should().Contain(remainingModifier);
            modifierContext.Active.Should().NotContain(expiringModifier);
        }
    }

    [Test]
    public void ModifierContext_WhenAddingSecondDoTModifier_DoesNotAdd()
    {
        TestDoTModifier existingModifier = new TestDoTModifier(ModifierLifespanDescription.Turns(100));
        TestDoTModifier newModifier = new TestDoTModifier(ModifierLifespanDescription.Turns(100));
        DamageResult damage = new(100, 0, 0);

        ModifierContext modifierContext = new();
        modifierContext.AddModifier(existingModifier, damage);

        bool added = modifierContext.AddModifier(newModifier, damage);

        using (new AssertionScope())
        {
            added.Should().BeFalse();
            modifierContext.Active.Should().OnlyContain(m => m == existingModifier);
        }
    }
}