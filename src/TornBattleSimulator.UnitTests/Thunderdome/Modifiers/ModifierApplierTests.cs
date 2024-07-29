using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
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
            new WeaponContextBuilder().WithModifier(healthMod).Build(),
            new DamageResult(100, 0, 0)
        );

        A.CallTo(() => healthMod.GetHealthModifier(A<PlayerContext>._, A<DamageResult>._))
            .MustHaveHappenedOnceExactly();
    }

    [TestCase(ModifierApplication.BeforeAction)]
    [TestCase(ModifierApplication.AfterAction)]
    public void ApplyModifiers_ForGivenPhase_AppliesCorrectModifiers(ModifierApplication phase)
    {
        // Arrange
        IModifier beforeModifier = A.Fake<IModifier>();
        A.CallTo(() => beforeModifier.AppliesAt).Returns(ModifierApplication.BeforeAction);
        A.CallTo(() => beforeModifier.Lifespan).Returns(ModifierLifespanDescription.Turns(1));
        A.CallTo(() => beforeModifier.ValueBehaviour).Returns(ModifierValueBehaviour.Chance);
        A.CallTo(() => beforeModifier.Target).Returns(ModifierTarget.Self);

        IModifier afterModifier = A.Fake<IModifier>();
        A.CallTo(() => afterModifier.AppliesAt).Returns(ModifierApplication.AfterAction);
        A.CallTo(() => afterModifier.Lifespan).Returns(ModifierLifespanDescription.Turns(1));
        A.CallTo(() => afterModifier.ValueBehaviour).Returns(ModifierValueBehaviour.Chance);
        A.CallTo(() => afterModifier.Target).Returns(ModifierTarget.Self);

        WeaponContext weapon = new WeaponContextBuilder()
           .WithModifier(beforeModifier)
           .WithModifier(afterModifier)
           .Build();

        PlayerContext active = new PlayerContextBuilder()
            .Build();

        PlayerContext other = new PlayerContextBuilder()
            .Build();

        ThunderdomeContext context = new ThunderdomeContextBuilder()
            .WithParticipants(active, other)
            .Build();

        ModifierApplier modifierApplier = new(FixedChanceSource.AlwaysSucceeds);

        // Act
        if (phase == ModifierApplication.BeforeAction)
        {
            modifierApplier.ApplyPreActionModifiers(context, active, other, weapon);
        }
        else
        {
            modifierApplier.ApplyPostActionModifiers(context, active, other, weapon, new DamageResult(100, BodyPart.Head, DamageFlags.HitArmour));
        }

        // Assert
        using (new AssertionScope())
        {
            if (phase == ModifierApplication.BeforeAction)
            {
                active.Modifiers.Active.Should().OnlyContain(m => m == beforeModifier);
            }
            else
            {
                active.Modifiers.Active.Should().OnlyContain(m => m == afterModifier);
            }
        }
        
    }
}