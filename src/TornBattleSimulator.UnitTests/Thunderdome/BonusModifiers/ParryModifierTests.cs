using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.BonusModifiers;

[TestFixture]
public class ParryModifierTests
{
    [Test]
    public void GetDamageModifier_BasedOnWeaponType_ReturnsValue([Values] WeaponType weaponType)
    {
        // Arrange
        WeaponContext weapon = new WeaponContextBuilder()
            .OfType(weaponType)
            .Build();

        AttackContext attack = new AttackContextBuilder()
            .WithWeapon(weapon)
            .Build();

        // Act
        double damage = new ParryModifier()
            .GetDamageModifier(attack, new HitLocation(0, null));

        // Assert
        if (weaponType == WeaponType.Melee)
        {
            damage.Should().Be(0);
        }
        else
        {
            damage.Should().Be(1);
        }
    }
}