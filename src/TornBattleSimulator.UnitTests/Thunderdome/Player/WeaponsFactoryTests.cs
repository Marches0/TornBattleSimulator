using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.Player;

[TestFixture]
public class WeaponsFactoryTests
{
    // Also tests WeaponContext
    [Test]
    public void GetContext_WithModifiers_AssignsCorrectly()
    {
        // Get all modifiers to really test it
        List<ModifierDescription> modifiers = Enum.GetValues<ModifierType>()
            .Select(t => new ModifierDescription() { Percent = 50, Type = t})
            .ToList();

        Weapon weapon = new()
        {
            Accuracy = 50,
            Damage = 50,
            Ammo = new Ammo()
            {
                Magazines = 1,
                MagazineSize = 1
            },
            RateOfFire = new RateOfFire()
            {
                Min = 1,
                Max = 1
            },
            Modifiers = modifiers
        };

        WeaponsFactory weaponsFactory = new WeaponsFactory(new ModifierFactory(), null);

        // Act
        WeaponContext weaponContext = weaponsFactory.GetContext(weapon, WeaponType.Primary);

        // Assert
        using (new AssertionScope())
        {
            int totalModifiers = weaponContext.Modifiers.Count + weaponContext.AlwaysActiveModifiers.Count;
            totalModifiers.Should().Be(modifiers.Count);

            weaponContext.AlwaysActiveModifiers.Should().AllSatisfy(m => m.ValueBehaviour.Should().NotBe(ModifierValueBehaviour.Chance));
            weaponContext.Modifiers.Should().AllSatisfy(m => m.Modifier.ValueBehaviour.Should().Be(ModifierValueBehaviour.Chance));
        }
    }
}