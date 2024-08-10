using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage;

[TestFixture]
public class DamageCalculatorTests
{
    [Test]
    public void CalculateDamage_CompoundsAndAddsDamageMultipliers()
    {
        // Arrange
        using AutoFake autoFake = new();
        autoFake.Provide<IEnumerable<IDamageModifier>>(new List<IDamageModifier>()
        {
            new TestDamageModifier(20, ModificationType.Additive),
            new TestDamageModifier(10, ModificationType.Additive),
            new TestDamageModifier(10, ModificationType.Multiplicative),
            new TestDamageModifier(0.5, ModificationType.Multiplicative),
        });

        DamageCalculator damageCalculator = autoFake.Resolve<DamageCalculator>();

        var attacker = new PlayerContextBuilder().Build();
        var defender = new PlayerContextBuilder().WithHealth(10000).Build();
        var weapon = new WeaponContextBuilder().WithModifier(new TestDamageModifier(0.5, ModificationType.Multiplicative)).Build();
        var attack = new AttackContextBuilder()
            .WithActive(attacker)
            .WithOther(defender)
            .WithWeapon(weapon)
            .Build();

        // Act
        var damage = damageCalculator.CalculateDamage(attack).DamageDealt;

        // Assert
        damage.Should().Be(75);
    }
}