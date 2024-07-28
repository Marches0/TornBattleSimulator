using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

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
            new StaticDamageModifier(20, StatModificationType.Additive),
            new StaticDamageModifier(10, StatModificationType.Additive),
            new StaticDamageModifier(10, StatModificationType.Multiplicative),
            new StaticDamageModifier(0.5, StatModificationType.Multiplicative),
        });

        DamageCalculator damageCalculator = autoFake.Resolve<DamageCalculator>();

        var attacker = new PlayerContextBuilder().Build();
        var defender = new PlayerContextBuilder().Build();
        var weapon = new WeaponContextBuilder().WithModifier(new StaticDamageModifier(0.5, StatModificationType.Multiplicative)).Build();

        // Act
        var damage = damageCalculator.CalculateDamage(new ThunderdomeContext(attacker, defender), attacker, defender, weapon).Damage;

        // Assert
        damage.Should().Be(75);
    }

    private class StaticDamageModifier : IDamageModifier, IModifier
    {
        private readonly double _multipler;

        public StaticDamageModifier(
            double multipler,
            StatModificationType type)
        {
            _multipler = multipler;
            Type = type;
        }

        /// <inheritdoc/>
        public StatModificationType Type { get; }

        public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

        public bool RequiresDamageToApply => throw new NotImplementedException();

        public ModifierTarget Target => ModifierTarget.Self;

        public ModifierApplication AppliesAt => throw new NotImplementedException();

        public ModifierType Effect => throw new NotImplementedException();

        public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

        public DamageModifierResult GetDamageModifier(PlayerContext active, PlayerContext other, WeaponContext weapon, DamageContext damageContext)
        {
            return new(_multipler);
        }
    }
}