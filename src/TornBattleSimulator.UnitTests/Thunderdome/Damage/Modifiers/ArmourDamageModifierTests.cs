using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.BonusModifiers.Armour;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.UnitTests.Chance;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage.Modifiers;

[TestFixture]
public class ArmourDamageModifierTests
{
    [TestCaseSource(nameof(ArmourDamageModifier_BasedOnArmourAndHit_WillMitigate_TestCases))]
    public void ArmourDamageModifier_BasedOnArmourAndHit_WillMitigate((BodyPart hitLocation, List<ArmourContext> armour, double expected, string testName) testData)
    {
        PlayerContext attacker = new PlayerContextBuilder().Build();
        PlayerContext defender = new PlayerContextBuilder().WithArmour(testData.armour).Build();

        ArmourDamageModifier armourDamageModifier = new ArmourDamageModifier(FixedChanceSource.AlwaysSucceeds);

        double mitigation = armourDamageModifier.GetDamageModifier(
            attacker,
            defender,
            new WeaponContextBuilder().Build(),
            new DamageContext() { TargetBodyPart = testData.hitLocation }
        );

        mitigation.Should().BeApproximately(testData.expected, 0.0001);
    }

    private static IEnumerable<(BodyPart hitLocation, List<ArmourContext> armour, double expected, string testName)> ArmourDamageModifier_BasedOnArmourAndHit_WillMitigate_TestCases()
    {
        yield return (BodyPart.Heart, new List<ArmourContext>(), 1d, "No armour doesn't mitigate");

        yield return (BodyPart.Heart, 
            new List<ArmourContext>() { 
                new ArmourContext(
                    1d,
                    // all but heart (which is hit) are covered
                    Enum.GetValues<BodyPart>().Where(bp => bp != BodyPart.Heart).Select(bp => new ArmourCoverage(){BodyPart = bp, Coverage = 1})
                .ToList(), new List<PotentialModifier>())
            }, 1d, "No mitigation where uncovered");

        yield return (BodyPart.Heart,
            new List<ArmourContext>()
            {
                new ArmourContext(0.9d, new List<ArmourCoverage>(){new ArmourCoverage() { BodyPart = BodyPart.Heart, Coverage = 1} }, new List<PotentialModifier>())
            }, 0.1d, "Mitigates where covered");
    }

    [TestCaseSource(nameof(ArmourDamageModifier_BasedOnCoverageChance_MitigatesAppropriately_TestCases))]
    public void ArmourDamageModifier_BasedOnCoverageChance_MitigatesAppropriately((List<ArmourContext> armour, double expected, string testName) testData)
    {
        PlayerContext attacker = new PlayerContextBuilder().Build();
        PlayerContext defender = new PlayerContextBuilder().WithArmour(testData.armour).Build();

        ArmourDamageModifier armourDamageModifier = new ArmourDamageModifier(FixedChanceSource.AlwaysSucceeds);

        double mitigation = armourDamageModifier.GetDamageModifier(
            attacker,
            defender,
            new WeaponContextBuilder().Build(),
            new DamageContext() { TargetBodyPart = BodyPart.Heart }
        );

        mitigation.Should().BeApproximately(testData.expected, 0.0001);
    }

    private static IEnumerable<(List<ArmourContext> armour, double expected, string testName)> ArmourDamageModifier_BasedOnCoverageChance_MitigatesAppropriately_TestCases()
    {
        yield return (
            new List<ArmourContext>()
            {
                new ArmourContext(
                    0.9,
                    new List<ArmourCoverage>() {new ArmourCoverage() { BodyPart = BodyPart.Heart, Coverage = 1} },
                    new List<PotentialModifier>()
                ),
                new ArmourContext(
                    0.6,
                    new List<ArmourCoverage>() {new ArmourCoverage() { BodyPart = BodyPart.Heart, Coverage = 1} },
                    new List<PotentialModifier>()
                ),
                new ArmourContext(
                    0.4,
                    new List<ArmourCoverage>() {new ArmourCoverage() { BodyPart = BodyPart.Heart, Coverage = 1} },
                    new List<PotentialModifier>()
                ),
            },
            0.1,
            "Strongest first"
        );

        yield return (
            new List<ArmourContext>()
            {
                new ArmourContext(
                    0.4,
                    new List<ArmourCoverage>() {new ArmourCoverage() { BodyPart = BodyPart.Heart, Coverage = 1} },
                    new List<PotentialModifier>()
                ),
                new ArmourContext(
                    0.6,
                    new List<ArmourCoverage>() {new ArmourCoverage() { BodyPart = BodyPart.Heart, Coverage = 1} },
                    new List<PotentialModifier>()
                ),
                new ArmourContext(
                    0.9,
                    new List<ArmourCoverage>() {new ArmourCoverage() { BodyPart = BodyPart.Heart, Coverage = 1} },
                    new List<PotentialModifier>()
                ),
            },
            0.1,
            "Strongest last"
        );
    }

    [TestCaseSource(nameof(ArmourDamageModifier_BasedOnModifiers_MitigatesAppropriately_TestCases))]
    public void ArmourDamageModifier_BasedOnModifiers_MitigatesAppropriately(
        (ArmourContext armour,
        List<IModifier> weaponModifiers,
        double expected,
        string testName) testData)
    {
        // Arrange
        WeaponContext weapon = new WeaponContextBuilder()
            .Build();
        
        foreach (var mod in testData.weaponModifiers)
        {
            weapon.Modifiers.AddModifier(mod, null);
        }

        PlayerContext attacker = new PlayerContextBuilder().Build();
        PlayerContext defender = new PlayerContextBuilder().WithArmour([testData.armour]).Build();

        ArmourDamageModifier armourDamageModifier = new ArmourDamageModifier(FixedChanceSource.AlwaysSucceeds);

        // Act
        double mitigation = armourDamageModifier.GetDamageModifier(
            attacker,
            defender,
            weapon,
            new DamageContext() { TargetBodyPart = BodyPart.Heart }
        );

        // Assert
        mitigation.Should().Be(testData.expected);
    }

    private static IEnumerable<(ArmourContext armour, List<IModifier> weaponModifiers, double expected, string testName)> ArmourDamageModifier_BasedOnModifiers_MitigatesAppropriately_TestCases()
    {
        const double baseArmourMitigation = 0.8;
        ArmourContext armour = new ArmourContext(baseArmourMitigation, [new ArmourCoverage() { BodyPart = BodyPart.Heart, Coverage = 1 }], []);

        yield return (
            armour,
            [ ],
            1 - baseArmourMitigation,
            "No modifier -> unchanged coverage"
        );

        yield return (
            armour,
            [ new PenetrateModifier(0.1) ],
            1 - (baseArmourMitigation * 0.9),
            "Penetrate 0.1 -> 90% remaining"
        );
    }
}