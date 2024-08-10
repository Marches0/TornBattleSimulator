using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Strategy;

namespace TornBattleSimulator.UnitTests.Thunderdome.BonusModifiers;

[TestFixture]
public class FinaleModifierTests
{
    [TestCaseSource(nameof(GetDamageModifier_BasedOnActionsSinceWeaponUsed_ReturnsModifier_TestCase))]
    public void GetDamageModifier_BasedOnActionsSinceWeaponUsed_ReturnsModifier((
        List<TurnActionHistory> actions,
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

        AttackContext attack = new AttackContextBuilder()
            .WithActive(attacker)
            .WithWeapon(weapon)
            .Build();

        // Act
        double damage = finale.GetDamageModifier(attack, new HitLocation(0, null));

        // Assert
        damage.Should().Be(testData.expected);
    }

    private static IEnumerable<(
        List<TurnActionHistory> actions,
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
            [ new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary), new TurnActionHistory(BattleAction.Attack, WeaponType.Primary) ],
            WeaponType.Primary,
            1,
            1,
            "Attacked last - primary"
        );

        yield return (
           [ new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary), new TurnActionHistory(BattleAction.Attack, WeaponType.Secondary)],
           WeaponType.Secondary,
           1,
           1,
           "Attacked last - secondary"
       );

        yield return (
           [new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary), new TurnActionHistory(BattleAction.Attack, WeaponType.Melee)],
           WeaponType.Melee,
           1,
           1,
           "Attacked last - melee"
       );

        yield return (
            [new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary), new TurnActionHistory(BattleAction.Reload, WeaponType.Primary)],
            WeaponType.Primary,
            1,
            1,
            "Reloaded last - primary"
        );

        yield return (
           [new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary), new TurnActionHistory(BattleAction.Reload, WeaponType.Secondary)],
           WeaponType.Secondary,
           1,
           1,
           "Reloaded last - secondary"
       );

        yield return (
            [new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary), new TurnActionHistory(BattleAction.Charge, WeaponType.Primary)],
            WeaponType.Primary,
            1,
            1,
            "Charged last - primary"
        );

        yield return (
           [new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary), new TurnActionHistory(BattleAction.Charge, WeaponType.Secondary)],
           WeaponType.Secondary,
           1,
           1,
           "Charged last - secondary"
       );

        yield return (
           [new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary), new TurnActionHistory(BattleAction.Charge, WeaponType.Melee)],
           WeaponType.Melee,
           1,
           1,
           "Charged last - melee"
       );

        yield return (
            [
                new TurnActionHistory(BattleAction.Attack, WeaponType.Primary),
                new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary),
                new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary),
                new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary),
                new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary)
            ],
            WeaponType.Primary,
            1,
            5,
            "Attacked a while ago - primary"
        );

        yield return (
            [
                 new TurnActionHistory(BattleAction.Attack, WeaponType.Secondary),
                new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary),
                new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary),
                new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary),
                new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary)
            ],
            WeaponType.Secondary,
            1,
            5,
            "Attacked a while ago - secondary"
        );

        yield return (
            [
                 new TurnActionHistory(BattleAction.Attack, WeaponType.Melee),
                 new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary),
                 new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary),
                 new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary),
                 new TurnActionHistory(BattleAction.Attack, WeaponType.Temporary)
            ],
            WeaponType.Melee,
            1,
            5,
            "Attacked a while ago - melee"
        );
    }
}