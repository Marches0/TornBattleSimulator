using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.BonusModifiers.Armour;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage.Modifiers;

[TestFixture]
public class ArmourDamageModifierTests
{
    [TestCaseSource(nameof(GetDamageModifier_BasedOnScenario_ReturnsModifier_TestCases))]
    public void GetDamageModifier_BasedOnScenario_ReturnsModifier((
        ArmourContext? armour,
        List<IModifier> weaponModifiers,
        List<IModifier> otherModifiers,
        double expectedModifier,
        string testName
        ) testData)
    {
        // Arrange
        PlayerContext other = new PlayerContextBuilder()
            .WithModifiers(testData.otherModifiers)
            .Build();

        WeaponContext weapon = new WeaponContextBuilder()
            .WithModifiers(testData.weaponModifiers)
            .Build();

        AttackContext attack = new AttackContextBuilder()
            .WithOther(other)
            .WithWeapon(weapon)
            .Build();

        ArmourDamageModifier armourDamageModifier = new();

        // Act
        double damageModifier = armourDamageModifier.GetDamageModifier(attack, new HitLocation(0, testData.armour));

        // Assert
        damageModifier.Should().Be(testData.expectedModifier);
    }

    private static IEnumerable<(
        ArmourContext? armour,
        List<IModifier> weaponModifiers,
        List<IModifier> otherModifiers,
        double expectedModifier,
        string testName
        )> GetDamageModifier_BasedOnScenario_ReturnsModifier_TestCases()
    {
        yield return (
            null,
            [],
            [],
            1,
            "Armour missed - 1"
        );

        yield return (
            new ArmourContext(0.2, [], []),
            [],
            [],
            0.8,
            "Regular armour hit - 0.8"
        );

        yield return (
            new ArmourContext(0.2, [], []),
            [ new PenetrateModifier(0.1) ],
            [],
            0.82,
            "Penetrate - reduces armour"
        );

        yield return (
            new ArmourContext(0.2, [], []),
            [new PenetrateModifier(0.1)],
            [ new PunctureModifier() ],
            1,
            "Puncture - ignores armour"
        );
    }
}