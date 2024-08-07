using FluentAssertions;
using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.BonusModifiers;

[TestFixture]
public class DisarmModifierTests
{
    [TestCaseSource(nameof(IsActive_BasedOnWeaponAndBodyPart_ReturnsValue_TestCases))]
    public void IsActive_BasedOnWeaponAndBodyPart_ReturnsValue((
        WeaponContext? otherWeapon,
        BodyPart hitBodyPart,
        bool active
        ) testData)
    {
        // Arrange
        PlayerContext other = new PlayerContextBuilder()
            .Build();

        other.ActiveWeapon = testData.otherWeapon;

        AttackContext attack = new AttackContextBuilder()
            .WithOther(other)
            .WithAttackResult(new AttackResult(true, 1, new DamageResult(1, testData.hitBodyPart, 0)))
            .Build();

        new DisarmModifier(2)
            .CanActivate(attack).Should().Be(testData.active);
    }

    private static IEnumerable<(
        WeaponContext? otherWeapon,
        BodyPart hitBodyPart,
        bool active
        )> IsActive_BasedOnWeaponAndBodyPart_ReturnsValue_TestCases()
    {
        foreach (BodyPart part in Enum.GetValues<BodyPart>())
        {
            yield return (null, part, false);
            yield return (new WeaponContextBuilder().Build(), part, part == BodyPart.Hands || part == BodyPart.Arms);
        }
    }
}