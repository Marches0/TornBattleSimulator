using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Strategy;
using TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Strategy;

[TestFixture]
public class UntilConditionResolverTests
{
    [TestCaseSource(nameof(Fulfilled_BasedOnModifierTargetActualTargetAndCount_ReturnsFulfilled_TestCases))]
    public void Fulfilled_BasedOnModifierTargetAndActualTarget_ReturnsFulfilled((
        List<IModifier> selfModifiers,
        List<IModifier> otherModifiers,
        ModifierTarget target,
        int targetCount,
        bool expected,
        string testName
    ) testData)
    {
        // Arrange
        TestTargetModifier modifier = new TestTargetModifier(testData.target);
        StrategyDescription strategy = new StrategyDescription()
        {
            Until = [new StrategyUntil() { Effect = modifier.Effect, Count = testData.targetCount }],
        };

        PlayerContext active = new PlayerContextBuilder()
            .Build();

        foreach (var activeModifier in testData.selfModifiers)
        {
            active.Modifiers.AddModifier(activeModifier, null);
        }

        PlayerContext other = new PlayerContextBuilder()
            .Build();

        foreach (var activeModifier in testData.otherModifiers)
        {
            other.Modifiers.AddModifier(activeModifier, null);
        }

        // Act
        var fulfilled = new UntilConditionResolver().Fulfilled(new AttackContextBuilder()
            .WithActive(active)
            .WithOther(other)
            .Build(), strategy);

        fulfilled.Should().Be(testData.expected);
    }

    private static IEnumerable<(
        List<IModifier> selfModifiers,
        List<IModifier> otherModifiers,
        ModifierTarget target,
        int targetCount,
        bool expected,
        string testName
    )> Fulfilled_BasedOnModifierTargetActualTargetAndCount_ReturnsFulfilled_TestCases()
    {
        yield return (
            [],
            [],
            ModifierTarget.Self,
            1,
            false,
            "Nothing applied - false"
        );

        yield return (
            [new TestTargetModifier(ModifierTarget.Self)],
            [],
            ModifierTarget.Self,
            1,
            true,
            "Applied to self on self modifier - true"
        );

        yield return (
            [],
            [new TestTargetModifier(ModifierTarget.Self)],
            ModifierTarget.Self,
            1,
            false,
            "Applied to other on self modifier - false"
        );

        yield return (
            [],
            [new TestTargetModifier(ModifierTarget.Other)],
            ModifierTarget.Other,
            1,
            true,
            "Applied to other on other modifier - true"
        );

        yield return (
            [new TestTargetModifier(ModifierTarget.Other)],
            [],
            ModifierTarget.Other,
            1,
            false,
            "Applied to self on other modifier - false"
        );

        yield return (
            [new TestTargetModifier(ModifierTarget.Self)],
            [],
            ModifierTarget.Self,
            2,
            false,
            "1 modifier where 2 wanted - false"
        );

        yield return (
            [new TestTargetModifier(ModifierTarget.Self), new TestTargetModifier(ModifierTarget.Self)],
            [],
            ModifierTarget.Self,
            2,
            true,
            "2 modifiers where 2 wanted - true"
        );

        yield return (
            [new TestTargetModifier(ModifierTarget.Self), new TestTargetModifier(ModifierTarget.Self), new TestTargetModifier(ModifierTarget.Self)],
            [],
            ModifierTarget.Self,
            2,
            true,
            "3 modifiers where 2 wanted - true"
        );
    }
}