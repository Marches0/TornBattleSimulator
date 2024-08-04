﻿using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers.Application;

[TestFixture]
public class ModifierApplierTests
{
    [TestCase(true)]
    [TestCase(false)]
    public void ApplyModifier_ForHealthModifier_MayApplyImmediately(bool appliesOnActivation)
    {
        // Arrange
        IHealthModifier testHealthModifier = new TestHealthModifier(appliesOnActivation);
        IHealthModifierApplier healthApplier = A.Fake<IHealthModifierApplier>();

        using AutoFake autoFake = new();
        autoFake.Provide(healthApplier);
        
        var applier = autoFake.Resolve<ModifierApplier>();

        var active = new PlayerContextBuilder().Build();
        var other = new PlayerContextBuilder().Build();
        var context = new ThunderdomeContextBuilder().WithParticipants(active, other).Build();
        var attack = new AttackContext(context, active, other, new WeaponContextBuilder().Build(), new AttackResult(true, 1, new DamageResult(1, BodyPart.Arms, DamageFlags.HitArmour)));

        // Act
        applier.ApplyModifier(
            testHealthModifier,
            attack
        );

        // Assert
        var call = A.CallTo(() => healthApplier.ModifyHealth(A<ThunderdomeContext>._, A<PlayerContext>._, testHealthModifier, A<AttackResult>._));
        if (appliesOnActivation)
        {
            call.MustHaveHappenedOnceExactly();
        }
        else
        {
            call.MustNotHaveHappened();
        }
    }

    [TestCase(true)]
    [TestCase(false)]
    public void ApplyOtherHeals_ForHealthModifier_MayApply(bool appliesOnActivation)
    {
        // Arrange
        IHealthModifier testHealthModifier = new TestHealthModifier(appliesOnActivation);
        IHealthModifierApplier healthApplier = A.Fake<IHealthModifierApplier>();

        using AutoFake autoFake = new();
        autoFake.Provide(healthApplier);

        var applier = autoFake.Resolve<ModifierApplier>();

        var active = new PlayerContextBuilder().Build();
        var other = new PlayerContextBuilder().Build();
        var context = new ThunderdomeContextBuilder().WithParticipants(active, other).Build();
        var weapon = new WeaponContextBuilder().WithModifier(testHealthModifier).Build();
        var attack = new AttackContext(context, active, other, weapon, new AttackResult(true, 1, new DamageResult(1, BodyPart.Arms, DamageFlags.HitArmour)));

        // Act
        applier.ApplyOtherHeals(
            attack
        );

        // Assert
        var call = A.CallTo(() => healthApplier.ModifyHealth(A<ThunderdomeContext>._, A<PlayerContext>._, testHealthModifier, A<AttackResult>._));
        if (appliesOnActivation)
        {
            call.MustNotHaveHappened();
        }
        else
        {
            call.MustHaveHappenedOnceExactly();
        }
    }

    [Test]
    public void ApplyFightStartModifiers_AppliesCorrectModifiers()
    {
        // Arrange
        // Add two of each type of modifier, to verify only the correct
        // type is added, and that it gets all of them.
        var modifiers = Enum.GetValues<ModifierApplication>().ToList()
            .SelectMany<ModifierApplication, TestModifierApplicationModifier>(a => [new TestModifierApplicationModifier(a), new TestModifierApplicationModifier(a)])
            .ToList();

        WeaponContext primary = new WeaponContextBuilder()
            .OfType(WeaponType.Primary)
            .WithModifiers(modifiers)
            .Build();

        WeaponContext secondary = new WeaponContextBuilder()
            .OfType(WeaponType.Secondary)
            .WithModifiers(modifiers)
            .Build();

        WeaponContext melee = new WeaponContextBuilder()
            .OfType(WeaponType.Melee)
            .WithModifiers(modifiers)
            .Build();

        WeaponContext temporary = new WeaponContextBuilder()
            .OfType(WeaponType.Temporary)
            .WithModifiers(modifiers)
            .Build();

        PlayerContext player = new PlayerContextBuilder()
            .WithPrimary(primary)
            .WithSecondary(secondary)
            .WithMelee(melee)
            .WithTemporary(temporary)
            .Build();

        ThunderdomeContext context = new ThunderdomeContextBuilder()
            .WithParticipants(player, new PlayerContextBuilder().Build())
            .Build();

        using AutoFake autoFake = new();
        ModifierApplier applier = autoFake.Resolve<ModifierApplier>();

        // Act
        var evts = applier.ApplyFightStartModifiers(context);

        // Assert
        using (new AssertionScope())
        {
            evts.Should()
                .HaveCount(8)
                .And.OnlyContain(e => e.Type == ThunderdomeEventType.EffectBegin);

            primary.Modifiers.Active.Should()
                .HaveCount(2)
                .And.OnlyContain(m => m.AppliesAt == ModifierApplication.FightStart);

            secondary.Modifiers.Active.Should()
                .HaveCount(2)
                .And.OnlyContain(m => m.AppliesAt == ModifierApplication.FightStart);

            melee.Modifiers.Active.Should()
                .HaveCount(2)
                .And.OnlyContain(m => m.AppliesAt == ModifierApplication.FightStart);

            temporary.Modifiers.Active.Should()
                .HaveCount(2)
                .And.OnlyContain(m => m.AppliesAt == ModifierApplication.FightStart);
        }
    }
}