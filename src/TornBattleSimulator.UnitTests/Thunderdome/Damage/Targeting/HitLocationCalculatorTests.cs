using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Options;
using TornBattleSimulator.UnitTests.Chance;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage.Targeting;

[TestFixture]
public class HitLocationCalculatorTests
{
    [TestCase(true, WeaponType.Primary, BodyPart.Head)]
    [TestCase(false, WeaponType.Primary, BodyPart.Groin)]
    [TestCase(true, WeaponType.Temporary, BodyPart.Chest)]
    public void GetHitLocation_BasedOnCritStatusAndWeaponType_ReturnsPart(
        bool isCrit, WeaponType weaponType, BodyPart expectedPart)
    {
        // Arrange
        IChanceSource chance = isCrit
            ? FixedChanceSource.AlwaysSucceeds
            : FixedChanceSource.AlwaysFails;

        
        BodyModifierOptions bodyOptions = new()
        {
            CriticalHits = [ new BodyPartDamage() { Part = BodyPart.Head } ],
            RegularHits = [
                // relies on the behaviour of FixedChanceSource returning the
                // first option for weighted choice.
                // should assert the chance call itself.
                new BodyPartDamage() { Part = BodyPart.Groin },
                new BodyPartDamage() { Part = BodyPart.Chest },
            ]
        };

        using AutoFake autoFake = new();
        autoFake.Provide(chance);
        autoFake.Provide(bodyOptions);

        HitLocationCalculator calculator = autoFake.Resolve<HitLocationCalculator>();

        AttackContext attack = new AttackContextBuilder()
            .WithWeapon(new WeaponContextBuilder().OfType(weaponType).Build())
            .Build();

        // Act
        BodyPart struckPart = calculator.GetHitLocation(attack);

        // Assert
        struckPart.Should().Be(expectedPart);
    }
}