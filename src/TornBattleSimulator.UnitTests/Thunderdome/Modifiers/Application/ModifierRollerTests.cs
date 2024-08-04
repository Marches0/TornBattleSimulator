using FakeItEasy;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.UnitTests.Chance;
using TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers.Application;

[TestFixture]
public class ModifierRollerTests
{
    [TestCase(ModifierApplication.BeforeAction)]
    [TestCase(ModifierApplication.AfterAction)]
    public void ApplyModifiers_ForGivenPhase_AppliesCorrectModifiers(ModifierApplication phase)
    {
        // Arrange
        IModifierApplier modifierApplier = A.Fake<IModifierApplier>();

        IModifier beforeModifier = GetFakeModifier(ModifierApplication.BeforeAction);
        IModifier afterModifier = GetFakeModifier(ModifierApplication.AfterAction);

        WeaponContext weapon = new WeaponContextBuilder()
           .WithModifier(beforeModifier)
           .WithModifier(afterModifier)
           .Build();

        PlayerContext active = new PlayerContextBuilder().Build();
        PlayerContext other = new PlayerContextBuilder().Build();
        ThunderdomeContext context = new ThunderdomeContextBuilder().WithParticipants(active, other).Build();
        AttackContext attack = new AttackContext(context, active, other, weapon, null);

        ModifierRoller modifierRoller = new ModifierRoller(FixedChanceSource.AlwaysSucceeds, modifierApplier);

        // Act
        if (phase == ModifierApplication.BeforeAction)
        {
            modifierRoller.ApplyPreActionModifiers(attack);
        }
        else
        {
            attack.AttackResult = new AttackResult(true, 1, new DamageResult(100, BodyPart.Head, DamageFlags.HitArmour));
            modifierRoller.ApplyPostActionModifiers(attack);
        }

        // Assert
        using (new AssertionScope())
        {
            var beforeModifierCall = GetModifierCall(modifierApplier, beforeModifier);
            var afterModifierCall = GetModifierCall(modifierApplier, afterModifier);

            if (phase == ModifierApplication.BeforeAction)
            {
                beforeModifierCall.MustHaveHappenedOnceExactly();
                afterModifierCall.MustNotHaveHappened();
            }
            else
            {
                afterModifierCall.MustHaveHappenedOnceExactly();
                beforeModifierCall.MustNotHaveHappened();
            }
        }
    }

    [TestCase(0, false)]
    [TestCase(1, true)]
    public void ApplyModifiers_BasedOnDamage_MayApplyModifier(int damageDone, bool appliedDamageModifier)
    {
        // Arrange
        IModifierApplier modifierApplier = A.Fake<IModifierApplier>();

        IModifier needsDamageModifier = GetFakeModifier(ModifierApplication.AfterAction);
        A.CallTo(() => needsDamageModifier.RequiresDamageToApply)
            .Returns(true);

        IModifier noDamageModifier = GetFakeModifier(ModifierApplication.AfterAction);
        A.CallTo(() => noDamageModifier.RequiresDamageToApply)
            .Returns(false);

        WeaponContext weapon = new WeaponContextBuilder()
            .WithModifier(needsDamageModifier)
            .WithModifier(noDamageModifier)
            .Build();

        PlayerContext active = new PlayerContextBuilder().Build();
        PlayerContext other = new PlayerContextBuilder().Build();
        ThunderdomeContext context = new ThunderdomeContextBuilder().WithParticipants(active, other).Build();
        DamageResult damage = new DamageResult(damageDone, BodyPart.Heart, DamageFlags.HitArmour);
        AttackContext attack = new AttackContext(context, active, other, weapon, new AttackResult(true, 1, damage));

        ModifierRoller roller = new ModifierRoller(FixedChanceSource.AlwaysSucceeds, modifierApplier);

        // Act
        roller.ApplyPostActionModifiers(
            attack);

        using (new AssertionScope())
        {
            GetModifierCall(modifierApplier, noDamageModifier).MustHaveHappenedOnceExactly();

            if (appliedDamageModifier)
            {
                GetModifierCall(modifierApplier, needsDamageModifier).MustHaveHappenedOnceExactly();
            }
            else
            {
                GetModifierCall(modifierApplier, needsDamageModifier).MustNotHaveHappened();
            }
        }
    }

    [TestCase(true)]
    [TestCase(false)]
    public void ApplyModifiers_BasedOnConditionFulfillment_MayApplyModifier(bool willActivate)
    {
        // Arrange
        IModifierApplier modifierApplier = A.Fake<IModifierApplier>();

        IConditionalModifier conditionalModifier = new TestConditionalModifier(willActivate);
        WeaponContext weapon = new WeaponContextBuilder()
            .WithModifier(conditionalModifier)
            .Build();

        ModifierRoller roller = new ModifierRoller(FixedChanceSource.AlwaysSucceeds, modifierApplier);
        PlayerContext active = new PlayerContextBuilder().WithPrimary(weapon).Build();
        PlayerContext other = new PlayerContextBuilder().Build();
        ThunderdomeContext context = new ThunderdomeContextBuilder().WithParticipants(active, other).Build();
        DamageResult damage = new DamageResult(123, BodyPart.Heart, DamageFlags.HitArmour);
        AttackContext attack = new AttackContext(context, active, other, weapon, new AttackResult(true, 1, damage));

        // Act
        roller.ApplyPostActionModifiers(attack);

        // Assert
        var call = GetModifierCall(modifierApplier, conditionalModifier);

        if (willActivate)
        {
            call.MustHaveHappenedOnceExactly();
        }
        else
        {
            call.MustNotHaveHappened();
        }
    }

    private IModifier GetFakeModifier(ModifierApplication modifierApplication)
    {
        IModifier modifier = A.Fake<IModifier>();
        A.CallTo(() => modifier.AppliesAt).Returns(modifierApplication);
        A.CallTo(() => modifier.Lifespan).Returns(ModifierLifespanDescription.Turns(1));
        A.CallTo(() => modifier.ValueBehaviour).Returns(ModifierValueBehaviour.Chance);
        A.CallTo(() => modifier.Target).Returns(ModifierTarget.Self);

        return modifier;
    }

    private FakeItEasy.Configuration.IReturnValueArgumentValidationConfiguration<List<Core.Thunderdome.Events.ThunderdomeEvent>>
        GetModifierCall(
        IModifierApplier modifierApplier,
        IModifier modifier)
    {
        return A.CallTo(() => modifierApplier.ApplyModifier(
            modifier,
            A<AttackContext>._)
        );
    }
}