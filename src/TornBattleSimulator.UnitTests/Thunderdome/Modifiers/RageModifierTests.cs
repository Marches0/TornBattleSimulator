﻿using FluentAssertions;
using TornBattleSimulator.BonusModifiers.Attacks;
using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;
using TornBattleSimulator.UnitTests.Chance;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;

[TestFixture]
public class RageModifierTests
{
    [Test]
    public void RageModifier_MakeAttack_AttacksChosenNumberOfTimes()
    {
        // Arrange
        int attackCount = 5;
        FixedChanceSource chanceSource = new FixedChanceSource(true, attackCount);
        RageModifier rageModifier = new(chanceSource);

        ThunderdomeContext context = new ThunderdomeContextBuilder().Build();
        PlayerContext active = new PlayerContextBuilder().Build();
        PlayerContext other = new PlayerContextBuilder().Build();
        WeaponContext weapon = new WeaponContextBuilder().Build();

        int attacksMade = 0;
        Func<List<ThunderdomeEvent>> attackAction = () => 
        {
            ++attacksMade;
            return [];
        };

        // Act
        rageModifier.MakeAttack(
            context,
            active,
            other,
            weapon,
            attackAction
        );

        // Assert
        attacksMade.Should().Be(attackCount);
    }
}