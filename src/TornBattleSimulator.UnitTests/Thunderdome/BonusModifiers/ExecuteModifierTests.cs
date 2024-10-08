﻿using FluentAssertions;
using TornBattleSimulator.BonusModifiers.Health;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.BonusModifiers;

[TestFixture]
public class ExecuteModifierTests
{
    [TestCase(1000, 1000, 0.5, false)]
    [TestCase(501, 1000, 0.5, false)]
    [TestCase(500, 1000, 0.5, true)]
    [TestCase(499, 1000, 0.5, true)]
    [TestCase(1000, 1000, 1, true)]
    public void IsActive_BasedOnOtherHealth_TrueOrFalse(
        int currentHealth,
        int maxHealth,
        double threshold,
        bool expected)
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

        new ExecuteModifier(threshold).CanActivate(attack).Should().Be(expected);
    }
}