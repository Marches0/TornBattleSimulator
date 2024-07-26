using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stackable;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

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
        PlayerContext player = new PlayerContextBuilder().Build();

        ModifierContext modifierContext = new(player);
        modifierContext.AddModifier(expiringModifier, null);
        modifierContext.AddModifier(remainingModifier, null);

        ThunderdomeContext thunderdomeContext = new ThunderdomeContextBuilder().WithParticipants(new PlayerContextBuilder(), new PlayerContextBuilder()).Build();

        // Act
        modifierContext.TurnComplete(thunderdomeContext);

        // Assert
        using (new AssertionScope())
        {
            modifierContext.Active.Should().Contain(remainingModifier);
            modifierContext.Active.Should().NotContain(expiringModifier);
        }
    }

    [Test]
    public void AddModifier_WhenAddingSecondDoTModifier_DoesNotAdd()
    {
        TestDoTModifier existingModifier = new TestDoTModifier(ModifierLifespanDescription.Turns(100));
        TestDoTModifier newModifier = new TestDoTModifier(ModifierLifespanDescription.Turns(100));
        DamageResult damage = new(100, 0, 0);
        PlayerContext player = new PlayerContextBuilder().Build();

        ModifierContext modifierContext = new(player);
        modifierContext.AddModifier(existingModifier, damage);

        bool added = modifierContext.AddModifier(newModifier, damage);

        using (new AssertionScope())
        {
            added.Should().BeFalse();
            modifierContext.Active.Should().OnlyContain(m => m == existingModifier);
        }
    }

    [Test]
    public void AddModifier_ForSecondStackOfStackingModifier_AddsStackToContainer()
    {
        // Arrange
        PlayerContext player = new PlayerContextBuilder().Build();
        TestStackableStatModifier modifier = new TestStackableStatModifier(1, 1, 1, 1, 5);
        ModifierContext modifierContext = new(player);

        // Stack 1
        modifierContext.AddModifier(modifier, null);

        // Act
        modifierContext.AddModifier(modifier, null);

        // Assert
        using(new AssertionScope())
        {
            modifierContext.Active.Should().OnlyContain(m => CorrectContainer(m, modifier));

            StackableStatModifierContainer container = (StackableStatModifierContainer)modifierContext.Active.Single();
            container.Stacks.Should().Be(2);
        }
    }
    private bool CorrectContainer(
        IModifier modifier,
        IModifier innerModifier)
    {
        return modifier is StackableStatModifierContainer c && c.Modifier == innerModifier;
    }
}