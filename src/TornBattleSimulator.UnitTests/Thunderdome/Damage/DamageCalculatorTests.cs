using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage;

[TestFixture]
public class DamageCalculatorTests
{
    [Test]
    public void DamageCalculator_CompoundsDamageMultipliers()
    {
        // Arrange
        using AutoFake autoFake = new();
        autoFake.Provide<IEnumerable<IDamageModifier>>(new List<IDamageModifier>()
        {
            new StaticDamageModifier(10),
            new StaticDamageModifier(5),
        });

        DamageCalculator damageCalculator = autoFake.Resolve<DamageCalculator>();

        var attacker = new PlayerContextBuilder().Build();
        var defender = new PlayerContextBuilder().Build();

        // Act
        var damage = damageCalculator.CalculateDamage(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        damage.Should().Be(50);
    }

    private class StaticDamageModifier : IDamageModifier
    {
        private readonly double _multipler;

        public StaticDamageModifier(double multipler)
        {
            _multipler = multipler;
        }

        public double GetDamageModifier(PlayerContext active, PlayerContext other)
        {
            return _multipler;
        }
    }
}