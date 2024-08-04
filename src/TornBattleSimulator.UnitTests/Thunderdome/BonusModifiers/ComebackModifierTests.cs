using FluentAssertions;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.BonusModifiers;

[TestFixture]
public class ComebackModifierTests
{
    [TestCase(100, 100, false)]
    [TestCase(26, 100, false)]
    [TestCase(25, 100, false)]
    [TestCase(24, 100, true)]
    public void IsActive_BasedOnOwnHealth_TrueOrFalse(int currentHealth, int maxHealth, bool expected)
    {
        PlayerContext owner = new PlayerContextBuilder()
            .WithHealth(maxHealth)
            .Build();

        owner.Health.CurrentHealth = currentHealth;

        new ComebackModifier(1).CanActivate(owner, new PlayerContextBuilder().Build(), new AttackResult(true, 1, new DamageResult(1, 0, 0)))
            .Should().Be(expected);
    }
}