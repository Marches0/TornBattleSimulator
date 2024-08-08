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
    [TestCaseSource(nameof(Fulfilled_BasedOnModifierTargetAndActualTarget_ReturnsFulfilled_TestCases))]
    public void Fulfilled_BasedOnModifierTargetAndActualTarget_ReturnsFulfilled((
        List<IModifier> selfModifiers,
        List<IModifier> otherModifiers,
        ModifierTarget target,
        bool expected,
        string testName
    ) testData)
    {
        // Arrange
        TestTargetModifier modifier = new TestTargetModifier(testData.target);
        StrategyDescription strategy = new StrategyDescription()
        {
            Until = [new StrategyUntil() { Effect = modifier.Effect }]
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
        bool expected,
        string testName
    )> Fulfilled_BasedOnModifierTargetAndActualTarget_ReturnsFulfilled_TestCases()
    {
        yield return (
            [],
            [],
            ModifierTarget.Self,
            false,
            "Nothing applied - false"
        );

        yield return (
            [new TestTargetModifier(ModifierTarget.Self)],
            [],
            ModifierTarget.Self,
            true,
            "Applied to self on self modifier - true"
        );

        yield return (
            [],
            [new TestTargetModifier(ModifierTarget.Self)],
            ModifierTarget.Self,
            false,
            "Applied to other on self modifier - false"
        );

        yield return (
            [],
            [new TestTargetModifier(ModifierTarget.Other)],
            ModifierTarget.Other,
            true,
            "Applied to other on other modifier - true"
        );

        yield return (
            [new TestTargetModifier(ModifierTarget.Other)],
            [],
            ModifierTarget.Other,
            false,
            "Applied to self on other modifier - false"
        );
    }
}