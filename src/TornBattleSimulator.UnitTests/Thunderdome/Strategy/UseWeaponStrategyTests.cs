using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;
using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.BonusModifiers.Ammo;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Strategy;
using TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Strategy;

[TestFixture]
public class UseWeaponStrategyTests
{
    [TestCaseSource(nameof(GetMove_BasedOnState_ReturnsMove_TestCases))]
    public void GetMove_BasedOnState_ReturnsMove((
        WeaponType weaponType,
        CurrentAmmo ammo,
        bool canReload,
        List<IModifier> weaponModifiers,
        BattleAction? expected,
        string testName
        ) testData)
    {
        // Arrange
        StrategyDescription strategy = new()
        {
            Weapon = testData.weaponType,
            Reload = testData.canReload
        };

        WeaponContext weapon = new WeaponContextBuilder()
            .OfType(testData.weaponType)
            .WithModifiers(testData.weaponModifiers)
            .Build();

        weapon.Ammo = testData.ammo;

        PlayerContext active = new PlayerContextBuilder()
            .WithWeapon(weapon)
            .Build();

        var useWeapon = new UseWeaponStrategy(strategy);

        TurnAction? action = useWeapon.GetMove(
            new ThunderdomeContextBuilder().Build(),
            active,
            new PlayerContextBuilder().Build()
        );

        // Assert
        using (new AssertionScope())
        {
            if (testData.expected == null)
            {
                action.Should().BeNull();
            }
            else
            {
                action.Action.Should().Be(testData.expected);
                action.Weapon.Should().Be(weapon);
            }
        }
    }

    private static IEnumerable<(
        WeaponType weaponType,
        CurrentAmmo? ammo,
        bool canReload,
        List<IModifier> weaponModifiers,
        BattleAction? expected,
        string testName
        )>
        GetMove_BasedOnState_ReturnsMove_TestCases()
    {
        // Disarm > Charge > Storage > Reload > Attack
        foreach (var weaponType in Enum.GetValues<WeaponType>())
        {
            yield return (
                weaponType,
                new CurrentAmmo()
                {
                    MagazineAmmoRemaining = 0,
                    MagazinesRemaining = 1
                },
                true,
                [],
                BattleAction.Reload,
                weaponType + ": Empty magazine with spare, and can reload - reload"
            );

            yield return (
                weaponType,
                new CurrentAmmo()
                {
                    MagazineAmmoRemaining = 0,
                    MagazinesRemaining = 1
                },
                false,
                [],
                null,
                weaponType + ": Empty magazine with spare, and cannot reload - null"
            );

            yield return (
                weaponType,
                new CurrentAmmo()
                {
                    MagazineAmmoRemaining = 0,
                    MagazinesRemaining = 0
                },
                true,
                [],
                null,
                weaponType + ": No ammo remaining, and can reload - null"
            );

            yield return (
                weaponType,
                new CurrentAmmo()
                {
                    MagazineAmmoRemaining = 1,
                    MagazinesRemaining = 1
                },
                true,
                [],
                BattleAction.Attack,
                weaponType + ": Ammo remaining - attack"
            );

            yield return (
                weaponType,
                null,
                true,
                [],
                BattleAction.Attack,
                weaponType + ": Weapon does not use ammo - attack"
            );

            yield return (
                weaponType,
                null,
                true,
                [ new DisarmModifier(1), new TestChargeableModifier(false), new StorageModifier() ],
                BattleAction.Disarmed,
                weaponType + ": Disarmed - Disarm"
            );

            yield return (
                weaponType,
                null,
                true,
                [ new TestChargeableModifier(false), new StorageModifier() ],
                BattleAction.Charge,
                weaponType + ": Requires charge - Charge"
            );

            yield return (
                weaponType,
                null,
                true,
                [ new StorageModifier()],
                BattleAction.ReplenishTemporary,
                weaponType + ": Storage - Replenish"
            );
        }
    }
}