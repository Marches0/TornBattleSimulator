using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers.Application;

[TestFixture]
public class HealthModifierApplierTests
{
    [TestCaseSource(nameof(ModifyHealth_StaysBetween0AndMax_TestCase))]
    public void ModifyHealth_StaysBetween0AndMax((
        int heal,
        int currentHealth,
        int maxHealth,
        int expectedHealth
    ) testData)
    {
        // Arrange
        IHealthModifier healthModifier = A.Fake<IHealthModifier>();
        A.CallTo(() => healthModifier.GetHealthModifier(A<PlayerContext>._, A<DamageResult>._))
            .Returns(testData.heal);

        PlayerContext player = new PlayerContextBuilder()
            .WithHealth(testData.maxHealth)
            .Build();

        player.Health.CurrentHealth = testData.currentHealth;

        // Act
        var @event = new HealthModifierApplier().ModifyHealth(
            new ThunderdomeContextBuilder()
            .WithParticipants(player, new PlayerContextBuilder().Build())
            .Build(),
            player,
            healthModifier,
            null
        );

        // Assert
        using (new AssertionScope())
        {
            player.Health.CurrentHealth.Should().Be(testData.expectedHealth);
            if (testData.heal >= 0)
            {
                @event.Type.Should().Be(ThunderdomeEventType.Heal);
            }
            else
            {
                @event.Type.Should().Be(ThunderdomeEventType.ExtraDamage);
            }
        }
    }

    private static IEnumerable<(
        int heal,
        int currentHealth,
        int maxHealth,
        int expectedHealth
    )> ModifyHealth_StaysBetween0AndMax_TestCase()
    {
        yield return (100, 100, 1000, 200);
        yield return (-50, 100, 1000, 50);
        yield return (999, 100, 1000, 1000);
        yield return (-999, 100, 1000, 0);
    }
}