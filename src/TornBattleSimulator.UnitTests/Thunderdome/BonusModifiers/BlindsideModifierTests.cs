using FluentAssertions;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.BonusModifiers;

[TestFixture]
public class BlindsideModifierTests
{
    [TestCase(100, 100, true)]
    [TestCase(99, 100, false)]
    public void IsActive_BasedOnOtherHealth_TrueOrFalse(int currentHealth, int maxHealth, bool expected)
    {
        PlayerContext other = new PlayerContextBuilder()
            .WithHealth(maxHealth)
            .Build();

        other.Health.CurrentHealth = currentHealth;

        AttackContext attack = new AttackContext(
            new ThunderdomeContextBuilder().Build(),
            new PlayerContextBuilder().Build(),
            other,
            new WeaponContextBuilder().Build(),
            null);

        new BlindsideModifier(1).CanActivate(attack)
            .Should().Be(expected);
    }
}