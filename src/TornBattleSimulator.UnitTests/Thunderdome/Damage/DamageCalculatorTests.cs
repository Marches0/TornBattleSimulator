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
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

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
            new StaticDamageModifier(0.5),
        });

        DamageCalculator damageCalculator = autoFake.Resolve<DamageCalculator>();

        var attacker = new PlayerContextBuilder().Build();
        var defender = new PlayerContextBuilder().Build();

        // Act
        var damage = damageCalculator.CalculateDamage(new ThunderdomeContext(attacker, defender), attacker, defender, null).Damage;

        // Assert
        damage.Should().Be(5);
    }

    private class StaticDamageModifier : IDamageModifier
    {
        private readonly double _multipler;

        public StaticDamageModifier(double multipler)
        {
            _multipler = multipler;
        }

        public DamageModifierResult GetDamageModifier(PlayerContext active, PlayerContext other, WeaponContext weapon)
        {
            return new(_multipler);
        }
    }
}