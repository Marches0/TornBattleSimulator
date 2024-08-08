using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;
using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.BonusModifiers.Stats.Temporary;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.UnitTests.Chance;

namespace TornBattleSimulator.UnitTests.Thunderdome.Strategy;

[TestFixture]
public class MissTurnStrategyTests
{
    [TestCaseSource(nameof(GetMove_BasedOnModifier_ReturnsAction_TestCases))]
    public void GetMove_BasedOnModifier_ReturnsAction((
        IModifier modifier,
        IChanceSource chanceSource,
        BattleAction? expected,
        string testName
        ) testData)
    {
        // Arrange
        MissTurnStrategy missTurn = new(testData.chanceSource);

        PlayerContext self = new PlayerContextBuilder()
            .Build();

        self.Modifiers.AddModifier(testData.modifier, null);

        // Act
        var turn = missTurn.GetMove(
            new ThunderdomeContextBuilder().Build(),
            self,
            new PlayerContextBuilder().Build()
        );

        // Assert
        turn?.Action.Should().Be(testData.expected);
    }

    private static IEnumerable<(
        IModifier modifier,
        IChanceSource chanceSource,
        BattleAction? expected,
        string testName
        )> GetMove_BasedOnModifier_ReturnsAction_TestCases()
    {
        yield return (new GassedModifier(), FixedChanceSource.AlwaysSucceeds, null, "No turn missing actions -> null");

        yield return (new ShockModifier(), FixedChanceSource.AlwaysFails, BattleAction.MissedTurn, "Shocked -> Turn Missed");
        yield return (new StunModifier(), FixedChanceSource.AlwaysFails, BattleAction.MissedTurn, "Stun -> Turn Missed");

        yield return (new ParalyzedModifier(), FixedChanceSource.AlwaysFails, null, "Paralyzed fails roll -> null");
        yield return (new ParalyzedModifier(), FixedChanceSource.AlwaysSucceeds, BattleAction.MissedTurn, "Paralyzed passes roll -> Turn Missed");

        yield return (new SuppressModifier(), FixedChanceSource.AlwaysFails, null, "Supress fails roll -> null");
        yield return (new SuppressModifier(), FixedChanceSource.AlwaysSucceeds, BattleAction.MissedTurn, "Supress passes roll -> Turn Missed");
    }
}