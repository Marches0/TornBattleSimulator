using FluentAssertions;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
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

        // Act
        double damage = new ParryModifier()
            .GetDamageModifier(
                new PlayerContextBuilder().Build(),
                new PlayerContextBuilder().Build(),
                weapon,
                new DamageContext()
        );

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