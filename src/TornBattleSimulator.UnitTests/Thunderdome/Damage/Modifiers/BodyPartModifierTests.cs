using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers.BodyParts;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Critical;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Options;
using TornBattleSimulator.UnitTests.Chance;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage.Modifiers;

[TestFixture]
public class BodyPartModifierTests
{
    [TestCaseSource(nameof(GetDamageModifier_BasedOnCrit_ReturnsModifier_TestCases))]
    public void GetDamageModifier_BasedOnCritAndWeapon_ReturnsModifier((bool isCrit, WeaponType weapon, BodyPart expected) testData)
    {
        // Arrange
        IChanceSource chanceSource = testData.isCrit
            ? FixedChanceSource.AlwaysSucceeds
            : FixedChanceSource.AlwaysFails;

        var options = new BodyModifierOptions()
        {
            CriticalHits = new() { new BodyPartDamage() { DamageMultiplier = 1, Part = BodyPart.Head } },
            RegularHits = new() { new BodyPartDamage() { DamageMultiplier = 1, Part = BodyPart.Stomach }, new BodyPartDamage() { DamageMultiplier = 1, Part = BodyPart.Chest } }
        };

        BodyPartModifier modifier = new(options, A.Fake<ICritChanceCalculator>(), chanceSource);

        WeaponContext weapon = new WeaponContextBuilder()
            .OfType(testData.weapon)
            .Build();

        DamageContext damageContext = new();
        
        // Act
        modifier.GetDamageModifier(new PlayerContextBuilder().Build(), new PlayerContextBuilder().Build(), weapon, damageContext);

        // Assert
        using (new AssertionScope())
        {
            damageContext.TargetBodyPart.Should().Be(testData.expected);
        }
    }

    private static IEnumerable<(bool isCrit, WeaponType weapon, BodyPart expected)> GetDamageModifier_BasedOnCrit_ReturnsModifier_TestCases()
    {
        yield return (false, WeaponType.Primary, BodyPart.Stomach);
        yield return (true, WeaponType.Primary, BodyPart.Head);
        yield return (true, WeaponType.Temporary, BodyPart.Chest);
    }

    public void GetDamageModifier_ForTemp_ReturnsChest((bool isCrit, BodyPart expected) testData)
    {

    }
}