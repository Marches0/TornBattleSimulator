using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome;
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
            new TestDamageModifier(20, StatModificationType.Additive),
            new TestDamageModifier(10, StatModificationType.Additive),
            new TestDamageModifier(10, StatModificationType.Multiplicative),
            new TestDamageModifier(0.5, StatModificationType.Multiplicative),
        });

        DamageCalculator damageCalculator = autoFake.Resolve<DamageCalculator>();

        var attacker = new PlayerContextBuilder().Build();
        var defender = new PlayerContextBuilder().WithHealth(10000).Build();
        var weapon = new WeaponContextBuilder().WithModifier(new TestDamageModifier(0.5, StatModificationType.Multiplicative)).Build();

        // Act
        var damage = damageCalculator.CalculateDamage(new ThunderdomeContext(attacker, defender), attacker, defender, weapon).DamageDealt;

        // Assert
        damage.Should().Be(75);
    }
}