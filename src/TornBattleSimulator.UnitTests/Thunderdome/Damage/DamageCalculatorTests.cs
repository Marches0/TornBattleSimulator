﻿using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage;

[TestFixture]
public class DamageCalculatorTests
{
    [Test]
    public void CalculateDamage_CompoundsDamageMultipliers()
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
        var weapon = new WeaponContextBuilder().WithModifier(new StaticDamageModifier(0.5)).Build();

        // Act
        var damage = damageCalculator.CalculateDamage(new ThunderdomeContext(attacker, defender), attacker, defender, weapon).Damage;

        // Assert
        damage.Should().Be(2);
    }

    private class StaticDamageModifier : IDamageModifier, IModifier
    {
        private readonly double _multipler;

        public StaticDamageModifier(double multipler)
        {
            _multipler = multipler;
        }

        public ModifierLifespanDescription Lifespan => throw new NotImplementedException();

        public bool RequiresDamageToApply => throw new NotImplementedException();

        public ModifierTarget Target => throw new NotImplementedException();

        public ModifierApplication AppliesAt => throw new NotImplementedException();

        public ModifierType Effect => throw new NotImplementedException();

        public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.None;

        public DamageModifierResult GetDamageModifier(PlayerContext active, PlayerContext other, WeaponContext weapon, DamageContext damageContext)
        {
            return new(_multipler);
        }
    }
}