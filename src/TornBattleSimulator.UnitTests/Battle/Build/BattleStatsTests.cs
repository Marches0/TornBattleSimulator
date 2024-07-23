using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Shared.Build;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.UnitTests.Thunderdome;

namespace TornBattleSimulator.UnitTests.Battle.Build;

[TestFixture]
public class BattleStatsTests
{
    [Test]
    public void BattleStats_Apply_AppliesModifier()
    {
        BattleStats stats = new BattleStats()
        {
            Strength = 10,
            Defence = 10,
            Speed = 10,
            Dexterity = 10
        };

        TestStatMultiplier mul = new TestStatMultiplier(0.5f, 3, 4, 5, StatModificationType.Multiplicative);

        stats.Apply(mul);

        using (new AssertionScope())
        {
            stats.Strength.Should().Be(5);
            stats.Defence.Should().Be(30);
            stats.Speed.Should().Be(40);
            stats.Dexterity.Should().Be(50);
        }
    }
}