using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;
using TornBattleSimulator.UnitTests.Chance;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage.Targeting;

[TestFixture]
public class HitArmourCalculatorTests
{
    [Test]
    public void GetHitArmour_ChecksStrongestApplicableArmour()
    {
        // Arrange
        BodyPart hitPart = BodyPart.Heart;

        ArmourContext strongestCoveringPart = new ArmourContext(
            100,
            [new() { BodyPart = hitPart, Coverage = 1 }],
            []
        );

        ArmourContext weakCoveringPart = new ArmourContext(
            1,
            [new() { BodyPart = hitPart, Coverage = 1 }],
            []
        );

        ArmourContext coversWrongPart = new ArmourContext(
            500,
            [new() { BodyPart = BodyPart.Feet, Coverage = 1 }],
            []
        );

        PlayerContext target = new PlayerContextBuilder()
            .WithArmour([strongestCoveringPart, weakCoveringPart, coversWrongPart])
            .Build();

        HitArmourCalculator calculator = new(FixedChanceSource.AlwaysSucceeds);

        // Act
        var armourHit = calculator.GetHitArmour(
            new AttackContextBuilder()
                .WithOther(target)
                .Build(),
            hitPart
        );

        // Assert
        armourHit.Should().Be(strongestCoveringPart);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void GetHitArmour_RollsAgainstCoverage(bool hitsArmour)
    {
        // Arrange
        BodyPart hitPart = BodyPart.Heart;
        ArmourContext armour = new ArmourContext(
            100,
            [new() { BodyPart = hitPart, Coverage = 1 }],
            []
        );

        PlayerContext target = new PlayerContextBuilder()
            .WithArmour([armour])
            .Build();

        HitArmourCalculator calculator = new(hitsArmour
            ? FixedChanceSource.AlwaysSucceeds
            : FixedChanceSource.AlwaysFails
        );

        // Act
        var armourHit = calculator.GetHitArmour(
            new AttackContextBuilder()
                .WithOther(target)
                .Build(),
            hitPart
        );

        // Assert
        if (hitsArmour)
        {
            armourHit.Should().Be(armour);
        }
        else
        {
            armourHit.Should().Be(null);
        }
    }
}