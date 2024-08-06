using FluentAssertions;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.BonusModifiers;

[TestFixture]
public class FinaleModifierTests
{
    [TestCaseSource(nameof(GetDamageModifier_BasedOnActionsSinceWeaponUsed_ReturnsModifier_TestCase))]
    public void GetDamageModifier_BasedOnActionsSinceWeaponUsed_ReturnsModifier((
        List<BattleAction> actions,
        WeaponType weaponType,
        double modifier,
        double expected,
        string testName
    ) testData)
    {
        // Arrange
        PlayerContext attacker = new PlayerContextBuilder()
            .Build();

        WeaponContext weapon = new WeaponContextBuilder()
            .OfType(testData.weaponType)
            .Build();

        attacker.Actions.AddRange(testData.actions);

        FinaleModifier finale = new(testData.modifier);

        // Act
        double damage = finale.GetDamageModifier(
            attacker,
            new PlayerContextBuilder().Build(),
            weapon,
            new DamageContext()
        );

        // Assert
        damage.Should().Be(testData.expected);
    }

    private static IEnumerable<(
        List<BattleAction> actions,
        WeaponType weaponType,
        double modifier,
        double expected,
        string testName
    )> GetDamageModifier_BasedOnActionsSinceWeaponUsed_ReturnsModifier_TestCase()
    {
        yield return (
            [],
            WeaponType.Primary,
            1,
            1,
            "No action history - primary"
        );

        yield return (
            [],
            WeaponType.Secondary,
            1,
            1,
            "No action history - secondary"
        );

        yield return (
            [],
            WeaponType.Melee,
            1,
            1,
            "No action history - melee"
        );

        yield return (
            [BattleAction.UseTemporary, BattleAction.AttackPrimary],
            WeaponType.Primary,
            1,
            1,
            "Attacked last - primary"
        );

        yield return (
           [BattleAction.UseTemporary, BattleAction.AttackSecondary],
           WeaponType.Secondary,
           1,
           1,
           "Attacked last - secondary"
       );

        yield return (
           [BattleAction.UseTemporary, BattleAction.AttackMelee],
           WeaponType.Melee,
           1,
           1,
           "Attacked last - melee"
       );

        yield return (
            [BattleAction.UseTemporary, BattleAction.ReloadPrimary],
            WeaponType.Primary,
            1,
            1,
            "Reloaded last - primary"
        );

        yield return (
           [BattleAction.UseTemporary, BattleAction.ReloadSecondary],
           WeaponType.Secondary,
           1,
           1,
           "Reloaded last - secondary"
       );

        yield return (
            [BattleAction.UseTemporary, BattleAction.ChargePrimary],
            WeaponType.Primary,
            1,
            1,
            "Charged last - primary"
        );

        yield return (
           [BattleAction.UseTemporary, BattleAction.ChargeSecondary],
           WeaponType.Secondary,
           1,
           1,
           "Charged last - secondary"
       );

        yield return (
           [BattleAction.UseTemporary, BattleAction.ChargeMelee],
           WeaponType.Melee,
           1,
           1,
           "Charged last - melee"
       );

        yield return (
            [BattleAction.AttackPrimary, BattleAction.UseTemporary, BattleAction.UseTemporary, BattleAction.UseTemporary, BattleAction.UseTemporary ],
            WeaponType.Primary,
            1,
            5,
            "Attacked a while ago - primary"
        );

        yield return (
            [BattleAction.AttackSecondary, BattleAction.UseTemporary, BattleAction.UseTemporary, BattleAction.UseTemporary, BattleAction.UseTemporary],
            WeaponType.Secondary,
            1,
            5,
            "Attacked a while ago - secondary"
        );

        yield return (
            [BattleAction.AttackMelee, BattleAction.UseTemporary, BattleAction.UseTemporary, BattleAction.UseTemporary, BattleAction.UseTemporary],
            WeaponType.Melee,
            1,
            5,
            "Attacked a while ago - melee"
        );
    }
}