﻿using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

namespace TornBattleSimulator.UnitTests.Thunderdome.Strategy;

[TestFixture]
public class CompositeStrategyTests
{
    [Test]
    public void CompositeStrategy_UsesFirstAcceptableStrategy()
    {
        // Arrange
        BattleAction expected = BattleAction.ThrowTemp;

        IStrategy firstUnusable = NullStrategy(A.Fake<IStrategy>());
        IStrategy secondUnusable = NullStrategy(A.Fake<IStrategy>());

        IStrategy firstUsable = A.Fake<IStrategy>();
        A.CallTo(() => firstUsable.GetMove(A<ThunderdomeContext>._, A<PlayerContext>._, A<PlayerContext>._))
            .Returns(expected);

        IStrategy firstIgnored = A.Fake<IStrategy>();

        PlayerContext attacker = new PlayerContextBuilder().Build();
        PlayerContext defender = new PlayerContextBuilder().Build();

        CompositeStrategy compositeStrategy = new CompositeStrategy([firstUnusable, secondUnusable, firstUsable, firstIgnored]);

        // Act
        BattleAction? action = compositeStrategy.GetMove(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        using (new AssertionScope())
        {
            action.Should().Be(expected);

            A.CallTo(() => firstUnusable.GetMove(A<ThunderdomeContext>._, A<PlayerContext>._, A<PlayerContext>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => secondUnusable.GetMove(A<ThunderdomeContext>._, A<PlayerContext>._, A<PlayerContext>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => firstUsable.GetMove(A<ThunderdomeContext>._, A<PlayerContext>._, A<PlayerContext>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => firstIgnored.GetMove(A<ThunderdomeContext>._, A<PlayerContext>._, A<PlayerContext>._))
                .MustNotHaveHappened();
        }
    }

    private IStrategy NullStrategy(IStrategy strategy)
    {
        A.CallTo(() => strategy.GetMove(A<ThunderdomeContext>._, A<PlayerContext>._, A<PlayerContext>._))
            .Returns(null);
        return strategy;
    }
}