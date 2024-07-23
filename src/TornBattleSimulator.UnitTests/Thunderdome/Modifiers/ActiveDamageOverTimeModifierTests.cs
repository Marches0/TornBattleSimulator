using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.BonusModifiers.DamageOverTime;
using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Damage;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;

[TestFixture]
public class ActiveDamageOverTimeModifierTests
{
    [Test]
    public void Tick_AppliesDecayingDamage_TurnAfterApplication()
    {
        uint startHealth = 10_000;
        int appliedDamage = 2000;

        PlayerContext target = new PlayerContextBuilder().WithHealth(startHealth).Build();
        PlayerContext other = new PlayerContextBuilder().Build();
        ThunderdomeContext thunderdomeContext = new ThunderdomeContextBuilder().WithParticipants(target, other).Build();

        ActiveDamageOverTimeModifier activeDotMod = new ActiveDamageOverTimeModifier(
            new TurnModifierLifespan(100),
            new SevereBurningModifier(),
            target,
            new DamageResult(appliedDamage, 0, 0)
        );

        // Act
        activeDotMod.OpponentActionComplete(thunderdomeContext);
        int sameTickHealth = target.Health.CurrentHealth;

        activeDotMod.TurnComplete(thunderdomeContext);

        activeDotMod.OpponentActionComplete(thunderdomeContext);
        int tickOneHealth = target.Health.CurrentHealth;

        activeDotMod.OpponentActionComplete(thunderdomeContext);
        int tickTwoHealth = target.Health.CurrentHealth;

        // Assert
        long tickOneDamage = startHealth - tickOneHealth;
        long tickTwoDamage = tickOneHealth - tickTwoHealth;

        using (new AssertionScope())
        {
            sameTickHealth.Should().Be((int)startHealth);
            tickOneDamage.Should().BeLessThan(appliedDamage);
            tickTwoDamage.Should().BeLessThan(tickOneDamage);
        }
    }
}