﻿using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Shared.Build.Equipment;
using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Damage;
using TornBattleSimulator.Shared.Thunderdome.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

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

    private class StaticDamageModifier : IDamageModifier, IAutoActivateModifier, IModifier
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

        public DamageModifierResult GetDamageModifier(PlayerContext active, PlayerContext other, WeaponContext weapon, DamageContext damageContext)
        {
            return new(_multipler);
        }
    }
}