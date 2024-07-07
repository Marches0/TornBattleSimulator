using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;

[TestFixture]
public class ActiveDamageOverTimeModifierTests
{
    [Test]
    public void ActiveDamageOverTimeModifier_Tick_AppliesDecayingDamage()
    {
        uint startHealth = 10_000;
        int appliedDamage = 2000;

        PlayerContext target = new PlayerContextBuilder().WithHealth(startHealth).Build();
        PlayerContext other = new PlayerContextBuilder().Build();
        ThunderdomeContext thunderdomeContext = new ThunderdomeContextBuilder().WithParticipants(target, other).Build();

        ActiveDamageOverTimeModifier activeDotMod = new ActiveDamageOverTimeModifier(
            new TurnModifierLifespan(100),
            new SevereBurningModifier(),
            new DamageResult(appliedDamage, 0, 0)
        );

        // Act
        activeDotMod.Tick(thunderdomeContext, target);
        int tickOneHealth = target.Health.CurrentHealth;

        activeDotMod.Tick(thunderdomeContext, target);
        int tickTwoHealth = target.Health.CurrentHealth;

        // Assert
        long tickOneDamage = startHealth - tickOneHealth;
        long tickTwoDamage = tickOneHealth - tickTwoHealth;

        using (new AssertionScope())
        {
            tickOneDamage.Should().BeLessThan(appliedDamage);
            tickTwoDamage.Should().BeLessThan(tickOneDamage);
        }
    }
}