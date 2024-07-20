﻿using FakeItEasy;
using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.UnitTests.Chance;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;

[TestFixture]
public class ModifierApplierTests
{
    [Test]
    public void ModifierApplier_ForHealingPostActionModifier_ImmediatelyHealsCorrectAmount()
    {
        PlayerContext active = new PlayerContextBuilder().Build();
        PlayerContext other = new PlayerContextBuilder().Build();
        ThunderdomeContext context = new ThunderdomeContextBuilder().WithParticipants(active, other).Build();

        IHealthModifier healthMod = A.Fake<IHealthModifier>();
        A.CallTo(() => healthMod.AppliesAt)
            .Returns(ModifierApplication.AfterAction);
        A.CallTo(() => healthMod.Lifespan)
            .Returns(ModifierLifespanDescription.Turns(1));

        ModifierApplier modifierApplier = new(FixedChanceSource.AlwaysSucceeds);

        modifierApplier.ApplyPostActionModifiers(
            context,
            active,
            other,
            new List<PotentialModifier>() { new PotentialModifier(healthMod, 1) },
            false,
            new DamageResult(100, 0, 0)
        );

        A.CallTo(() => healthMod.GetHealthMod(A<PlayerContext>._, A<DamageResult>._))
            .MustHaveHappenedOnceExactly();
    }

    [TestCase(true)]
    [TestCase(false)]
    public void ModifierApplier_ForAttackActions_OnlyAppliesInRegularActions(bool isBonusAction)
    {
        // Arrange
        PlayerContext active = new PlayerContextBuilder().Build();
        PlayerContext other = new PlayerContextBuilder().Build();
        ThunderdomeContext context = new ThunderdomeContextBuilder().WithParticipants(active, other).Build();

        IAttacksModifier attackModifier = A.Fake<IAttacksModifier>();
        A.CallTo(() => attackModifier.AppliesAt)
            .Returns(ModifierApplication.AfterAction);
        A.CallTo(() => attackModifier.Lifespan)
            .Returns(ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterCurrentAction));
        A.CallTo(() => attackModifier.Target)
            .Returns(ModifierTarget.Self);

        ModifierApplier modifierApplier = new(FixedChanceSource.AlwaysSucceeds);

        // Act
        modifierApplier.ApplyPostActionModifiers(
            context,
            active,
            other,
            new List<PotentialModifier>() { new PotentialModifier(attackModifier, 1) },
            isBonusAction,
            new DamageResult(100, 0, 0)
        );

        // Assert
        if (isBonusAction)
        {
            active.Modifiers.Active.Should().BeEmpty();
        }
        else
        {
            active.Modifiers.Active.Should().Contain(attackModifier);
        }
    }
}