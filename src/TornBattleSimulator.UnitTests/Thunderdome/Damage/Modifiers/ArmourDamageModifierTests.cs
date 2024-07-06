using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Chance;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Player.Armours;
using TornBattleSimulator.UnitTests.Chance;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage.Modifiers;

[TestFixture]
public class ArmourDamageModifierTests
{
    private readonly IChanceSource _guaranteeSuccess = new FixedChanceSource(true);

    [TestCaseSource(nameof(ArmourDamageModifier_BasedOnArmourAndHit_WillMitigate_TestCases))]
    public void ArmourDamageModifier_BasedOnArmourAndHit_WillMitigate((BodyPart hitLocation, List<ArmourContext> armour, double expected, string testName) testData)
    {
        PlayerContext attacker = new PlayerContextBuilder().Build();
        PlayerContext defender = new PlayerContextBuilder().WithArmour(testData.armour).Build();

        ArmourDamageModifier armourDamageModifier = new ArmourDamageModifier(_guaranteeSuccess);

        double mitigation = armourDamageModifier.GetDamageModifier(attacker, defender, null, new DamageContext() { TargetBodyPart = testData.hitLocation }).Multiplier;

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
        // Multiple pieces cover the same area - we take the highest rating one
        // for damage mitigation.
        // Is that correct? No idea.
        PlayerContext attacker = new PlayerContextBuilder().Build();
        PlayerContext defender = new PlayerContextBuilder().WithArmour(testData.armour).Build();

        ArmourDamageModifier armourDamageModifier = new ArmourDamageModifier(_guaranteeSuccess);

        double mitigation = armourDamageModifier.GetDamageModifier(attacker, defender, null, new DamageContext() { TargetBodyPart = BodyPart.Heart }).Multiplier;

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
}