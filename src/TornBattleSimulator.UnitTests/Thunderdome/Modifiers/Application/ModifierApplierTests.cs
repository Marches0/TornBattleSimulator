using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Player;
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

        // Act
        applier.ApplyModifier(
            testHealthModifier,
            context,
            active,
            other,
            new WeaponContextBuilder().Build(),
            new AttackResult(true, 1, new DamageResult(1, BodyPart.Arms, DamageFlags.HitArmour))
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
        
        // Act
        applier.ApplyOtherHeals(
            context,
            active,
            other,
            weapon,
            new AttackResult(true, 1, new DamageResult(1, BodyPart.Arms, DamageFlags.HitArmour))
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
}