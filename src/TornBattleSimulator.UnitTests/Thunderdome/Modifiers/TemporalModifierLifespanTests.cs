using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;

[TestFixture]
public class TemporalModifierLifespanTests
{
    [Test]
    public void TemporaalModifierLifespan_WhenTimeRemaining_NotExpired()
    {
        TemporalModifierLifespan mod = new TemporalModifierLifespan(10);

        mod.Expired.Should().BeFalse();
    }

    [Test]
    public void TemporaalModifierLifespan_WhenTickedBelowRemaining_Expires()
    {
        TemporalModifierLifespan mod = new TemporalModifierLifespan(0.5f);
        ThunderdomeContext context = new ThunderdomeContextBuilder().WithParticipants(new PlayerContextBuilder(), new PlayerContextBuilder()).Build();

        mod.Expired.Should().BeFalse();

        mod.Tick(context);

        mod.Expired.Should().BeTrue();
    }
}