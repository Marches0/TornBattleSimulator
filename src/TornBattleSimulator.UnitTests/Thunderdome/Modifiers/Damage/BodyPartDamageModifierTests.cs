using FluentAssertions;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers.Damage;

[TestFixture]
public class BodyPartDamageModifierTests
{
    [TestCaseSource(nameof(GetDamageModifier_BasedOnBodyPart_DealsBonusDamage_TestCases))]
    public void GetDamageModifier_BasedOnBodyPart_DealsBonusDamage((double bonus, BodyPart target, BodyPart actual, double expected, string testName) testData)
    {
        // Arrange
        TestBodyPartDamageModifier testBodyPartDamageModifier = new(testData.target, testData.bonus);

        // Act
        DamageModifierResult modifier = testBodyPartDamageModifier.GetDamageModifier(
            new PlayerContextBuilder().Build(),
            new PlayerContextBuilder().Build(),
            new WeaponContextBuilder().Build(),
            new DamageContext() { TargetBodyPart = testData.actual });

        // Assert
        modifier.Multiplier.Should().Be(testData.expected);
    }

    private static IEnumerable<(double bonus, BodyPart target, BodyPart actual, double expected, string testName)>
        GetDamageModifier_BasedOnBodyPart_DealsBonusDamage_TestCases()
    {
        yield return (0.5, BodyPart.Heart, BodyPart.Heart, 1.5, "Hits bonus damage part");
        yield return (0.5, BodyPart.Heart, BodyPart.Feet, 1, "Misses bonus damage part");
    }
}