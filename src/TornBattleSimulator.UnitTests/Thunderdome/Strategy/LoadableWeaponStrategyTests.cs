using FluentAssertions;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Description;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

namespace TornBattleSimulator.UnitTests.Thunderdome.Strategy;

[TestFixture]
public class LoadableWeaponStrategyTests : LoadableWeaponTests
{
    [TestCaseSource(nameof(PrimaryWeaponStrategy_BasedOnStatus_PerformsAction_TestData))]
    public void PrimaryWeaponStrategy_BasedOnStatus_PerformsAction((int currentMagazineAmmo, int magazinesRemaining, bool canReload, BattleAction? expected, string testName) testData)
    {
        // Arrange
        PlayerContext attacker = new PlayerContextBuilder().WithPrimary(GetLoadableWeapon()).Build();
        PlayerContext defender = new PlayerContextBuilder().Build();

        attacker.Primary!.Ammo.MagazineAmmoRemaining = testData.currentMagazineAmmo;
        attacker.Primary!.Ammo.MagazinesRemaining = testData.magazinesRemaining;

        // Act
        BattleAction? action = new PrimaryWeaponStrategy(GetStrategyDescription(WeaponType.Primary, testData.canReload)).GetMove(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        action.Should().Be(testData.expected);
    }

    [TestCaseSource(nameof(SecondaryWeaponStrategy_BasedOnStatus_PerformsAction_TestData))]
    public void SecondaryWeaponStrategy_BasedOnStatus_PerformsAction((int currentMagazineAmmo, int magazinesRemaining, bool canReload, BattleAction? expected, string testName) testData)
    {
        // Arrange
        PlayerContext attacker = new PlayerContextBuilder().WithSecondary(GetLoadableWeapon()).Build();
        PlayerContext defender = new PlayerContextBuilder().Build();

        attacker.Secondary!.Ammo.MagazineAmmoRemaining = testData.currentMagazineAmmo;
        attacker.Secondary!.Ammo.MagazinesRemaining = testData.magazinesRemaining;

        // Act
        BattleAction? action = new SecondaryWeaponStrategy(GetStrategyDescription(WeaponType.Secondary, testData.canReload)).GetMove(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        action.Should().Be(testData.expected);
    }

    private static IEnumerable<(int currentMagazineAmmo, int magazinesRemaining, bool canReload, BattleAction? expected, string testName)> PrimaryWeaponStrategy_BasedOnStatus_PerformsAction_TestData()
    {
        yield return (10, 1, false, BattleAction.AttackPrimary, "Attacks when ammo remaining 1");
        yield return (10, 1, true, BattleAction.AttackPrimary, "Attacks when ammo remaining 2");

        yield return (0, 1, true, BattleAction.ReloadPrimary, "Reloads when empty magazine and can reload");
        yield return (0, 1, false, null, "Skips when empty magazine and can't reload");

        yield return (0, 0, false, null, "Skips when empty magazine and no magazines remaining 1");
        yield return (0, 0, true, null, "Skips when empty magazine and no magazines remaining 2");
    }

    private static IEnumerable<(int currentMagazineAmmo, int magazinesRemaining, bool canReload, BattleAction? expected, string testName)> SecondaryWeaponStrategy_BasedOnStatus_PerformsAction_TestData()
    {
        yield return (10, 1, false, BattleAction.AttackSecondary, "Attacks when ammo remaining 1");
        yield return (10, 1, true, BattleAction.AttackSecondary, "Attacks when ammo remaining 2");

        yield return (0, 1, true, BattleAction.ReloadSecondary, "Reloads when empty magazine and can reload");
        yield return (0, 1, false, null, "Skips when empty magazine and can't reload");

        yield return (0, 0, false, null, "Skips when empty magazine and no magazines remaining 1");
        yield return (0, 0, true, null, "Skips when empty magazine and no magazines remaining 2");
    }

    private StrategyDescription GetStrategyDescription(WeaponType weaponType, bool canReload)
    {
        return new()
        {
            Weapon = weaponType,
            Reload = canReload,
            Until = new List<StrategyUntil>()
        };
    }
}