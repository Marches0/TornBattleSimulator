using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats;

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

        TestStatMultiplier mul = new TestStatMultiplier(0.5f, 3, 4, 5);

        stats.Apply(mul);

        using (new AssertionScope())
        {
            stats.Strength.Should().Be(5);
            stats.Defence.Should().Be(30);
            stats.Speed.Should().Be(40);
            stats.Dexterity.Should().Be(50);
        }
    }

    private class TestStatMultiplier : IStatsModifier
    {
        private readonly float _strengthModifier;
        private readonly float _defenceModifier;
        private readonly float _speedModifier;
        private readonly float _dexterityModifier;

        public TestStatMultiplier(
            float strengthModifier,
            float defenceModifier,
            float speedModifier,
            float dexterityModifier)
        {
            _strengthModifier = strengthModifier;
            _defenceModifier = defenceModifier;
            _speedModifier = speedModifier;
            _dexterityModifier = dexterityModifier;
        }

        public ModifierLifespanDescription Lifespan => throw new NotImplementedException();

        public bool RequiresDamageToApply => throw new NotImplementedException();

        public ModifierTarget Target => throw new NotImplementedException();

        public ModifierApplication AppliesAt => throw new NotImplementedException();

        public ModifierType Effect => throw new NotImplementedException();

        public float GetDefenceModifier() => _defenceModifier;

        public float GetDexterityModifier() => _dexterityModifier;

        public float GetSpeedModifier() => _speedModifier;

        public float GetStrengthModifier() => _strengthModifier;
    }
}