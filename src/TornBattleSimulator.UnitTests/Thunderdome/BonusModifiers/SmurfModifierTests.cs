using FluentAssertions;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.BonusModifiers;

[TestFixture]
public class SmurfModifierTests
{
    [TestCaseSource(nameof(GetDamageModifier_BasedOnLevelDifference_ReturnsModifier_TestCases))]
    public void GetDamageModifier_BasedOnLevelDifference_ReturnsModifier((
        int attackerLevel,
        int defenderLevel,
        double modifierValue,
        double expected,
        string testName) testData)
    {
        // Arrange
        PlayerContext attacker = new PlayerContextBuilder()
            .WithLevel(testData.attackerLevel)
            .Build();

        PlayerContext defender = new PlayerContextBuilder()
            .WithLevel(testData.defenderLevel)
            .Build();

        WeaponContext weapon = new WeaponContextBuilder()
            .Build();

        // Act
        double modifier = new SmurfModifier(testData.modifierValue)
            .GetDamageModifier(attacker, defender, weapon, new DamageContext());

        // Assert
        modifier.Should().Be(testData.expected);
    }

    private static IEnumerable<(
        int attackerLevel,
        int defenderLevel,
        double modifierValue,
        double expected,
        string testName)> GetDamageModifier_BasedOnLevelDifference_ReturnsModifier_TestCases()
    {
        const int defenderLevel = 50;

        yield return (
            defenderLevel,
            defenderLevel,
            0.2,
            1,
            "Same level -> no bonus"
        );

        yield return (
            defenderLevel + 1,
            defenderLevel,
            0.2,
            1,
            "Higher level -> no bonus"
        );

        yield return (
            defenderLevel - 1,
            defenderLevel,
            0.2,
            1.2,
            "1 level lower"
        );

        yield return (
           defenderLevel - 2,
           defenderLevel,
           0.2,
           1.4,
           "2 levels lower"
       );

        yield return (
           defenderLevel - 10,
           defenderLevel,
           0.2,
           3,
           "10 levels lower"
       );
    }
}