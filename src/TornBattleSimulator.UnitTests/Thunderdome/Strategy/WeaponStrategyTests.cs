/*using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;
using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Strategy;
using TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Strategy;

[TestFixture]
public class WeaponStrategyTests : LoadableWeaponTests
{
    [TestCaseSource(nameof(PrimaryWeaponStrategy_BasedOnStatus_PerformsAction_TestData))]
    public void PrimaryWeaponStrategy_BasedOnStatus_PerformsAction((int currentMagazineAmmo, int magazinesRemaining, bool canReload, bool charged, bool disarmed, BattleAction? expected, string testName) testData)
    {
        // Arrange
        WeaponContext weapon = new WeaponContextBuilder()
            .WithAmmo(10, 10)
            .WithModifier(new TestChargeableModifier(testData.charged))
            .Build();

        ThunderdomeContext thunderdome = new ThunderdomeContextBuilder().Build();
        PlayerContext attacker = new PlayerContextBuilder().WithPrimary(weapon).Build();
        PlayerContext defender = new PlayerContextBuilder().Build();

        weapon.Modifiers = new ModifierContext(attacker);
        weapon.Modifiers.AddModifier(new TestChargeableModifier(testData.charged), null);

        attacker.Weapons.Primary!.Ammo.MagazineAmmoRemaining = testData.currentMagazineAmmo;
        attacker.Weapons.Primary!.Ammo.MagazinesRemaining = testData.magazinesRemaining;

        if (testData.disarmed)
        {
            weapon.Modifiers.AddModifier(new DisarmModifier(10), null);
        }

        // Act
        BattleAction? action = new PrimaryWeaponStrategy(GetStrategyDescription(WeaponType.Primary, testData.canReload)).GetMove(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        action.Should().Be(testData.expected);
    }

    [TestCaseSource(nameof(SecondaryWeaponStrategy_BasedOnStatus_PerformsAction_TestData))]
    public void SecondaryWeaponStrategy_BasedOnStatus_PerformsAction((int currentMagazineAmmo, int magazinesRemaining, bool canReload, bool charged, bool disarmed, BattleAction? expected, string testName) testData)
    {
        // Arrange
        WeaponContext weapon = new WeaponContextBuilder()
            .WithAmmo(10, 10)
            .WithModifier(new TestChargeableModifier(testData.charged))
            .Build();

        ThunderdomeContext thunderdome = new ThunderdomeContextBuilder().Build();
        PlayerContext attacker = new PlayerContextBuilder().WithSecondary(weapon).Build();
        PlayerContext defender = new PlayerContextBuilder().Build();

        weapon.Modifiers = new ModifierContext(attacker);
        weapon.Modifiers.AddModifier(new TestChargeableModifier(testData.charged), null);

        if (testData.disarmed)
        {
            weapon.Modifiers.AddModifier(new DisarmModifier(10), null);
        }

        attacker.Weapons.Secondary!.Ammo.MagazineAmmoRemaining = testData.currentMagazineAmmo;
        attacker.Weapons.Secondary!.Ammo.MagazinesRemaining = testData.magazinesRemaining;

        // Act
        BattleAction? action = new SecondaryWeaponStrategy(GetStrategyDescription(WeaponType.Secondary, testData.canReload)).GetMove(new ThunderdomeContext(attacker, defender), attacker, defender);

        // Assert
        action.Should().Be(testData.expected);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void MeleeWeaponStrategy_ReturnsAttackOrDisarmed(bool disarmed)
    {
        var weapon = new WeaponContextBuilder().Build();

        if (disarmed)
        {
            weapon.Modifiers.AddModifier(new DisarmModifier(2), null);
        }

        PlayerContext attacker = new PlayerContextBuilder().WithMelee(weapon).Build();
        PlayerContext defender = new PlayerContextBuilder().Build();
        
        BattleAction? action = new MeleeWeaponStrategy(new StrategyDescription()).GetMove(new ThunderdomeContext(attacker, defender), attacker, defender);

        if (disarmed)
        {
            action.Should().Be(BattleAction.DisarmMelee);
        }
        else
        {
            action.Should().Be(BattleAction.AttackMelee);
        }
    }

    [TestCaseSource(nameof(TemporaryWeaponStrategy_BasedOnStatus_PerformsAction_TestData))]
    public void TemporaryWeaponStrategy_BasedOnStatus_PerformsAction((int currentMagazineAmmo, BattleAction? expected, string testName) testData)
    {
        PlayerContext attacker = new PlayerContextBuilder().WithTemporary(new WeaponContextBuilder().WithAmmo(0, 1).Build()).Build();
        PlayerContext defender = new PlayerContextBuilder().Build();

        attacker.Weapons.Temporary.Ammo.MagazineAmmoRemaining = testData.currentMagazineAmmo;

        // Act
        BattleAction? action = new TemporaryWeaponStrategy(new StrategyDescription() { Weapon = WeaponType.Temporary })
            .GetMove(new ThunderdomeContextBuilder().WithParticipants(attacker, defender).Build(), attacker, defender);

        // Assert
        action.Should().Be(testData.expected);
    }

    private static IEnumerable<(int currentMagazineAmmo, int magazinesRemaining, bool canReload, bool charged, bool disarmed, BattleAction? expected, string testName)> PrimaryWeaponStrategy_BasedOnStatus_PerformsAction_TestData()
    {
        yield return (10, 1, false, true, false, BattleAction.AttackPrimary, "Attacks when ammo remaining 1");
        yield return (10, 1, true, true, false, BattleAction.AttackPrimary, "Attacks when ammo remaining 2");

        yield return (0, 1, true, true, false, BattleAction.ReloadPrimary, "Reloads when empty magazine and can reload");
        yield return (0, 1, false, true, false, null, "Skips when empty magazine and can't reload");

        yield return (0, 0, false, true, false, null, "Skips when empty magazine and no magazines remaining 1");
        yield return (0, 0, true, true, false, null, "Skips when empty magazine and no magazines remaining 2");

        yield return (10, 1, false, false, false, BattleAction.ChargePrimary, "Charges when ammo remaining and needs charge");
        yield return (0, 1, true, false, false, BattleAction.ChargePrimary, "Charges when needs reload and needs charge");

        yield return (0, 0, false, false, false, null, "Does nothing when needs to charge, but cannot reload or attack");

        yield return (10, 1, false, true, true, BattleAction.DisarmPrimary, "Disarmed when ammo remaining and disarmed");
        yield return (0, 1, true, true, true, BattleAction.DisarmPrimary, "Disarmed when want to reload remaining and disarmed");
    }

    private static IEnumerable<(int currentMagazineAmmo, int magazinesRemaining, bool canReload, bool charged, bool disarmed, BattleAction? expected, string testName)> SecondaryWeaponStrategy_BasedOnStatus_PerformsAction_TestData()
    {
        yield return (10, 1, false, true, false, BattleAction.AttackSecondary, "Attacks when ammo remaining 1");
        yield return (10, 1, true, true, false, BattleAction.AttackSecondary, "Attacks when ammo remaining 2");

        yield return (0, 1, true, true, false, BattleAction.ReloadSecondary, "Reloads when empty magazine and can reload");
        yield return (0, 1, false, true, false, null, "Skips when empty magazine and can't reload");

        yield return (0, 0, false, true, false, null, "Skips when empty magazine and no magazines remaining 1");
        yield return (0, 0, true, true, false, null, "Skips when empty magazine and no magazines remaining 2");

        yield return (10, 1, false, false, false, BattleAction.ChargeSecondary, "Charges when ammo remaining and needs charge");
        yield return (0, 1, true, false, false, BattleAction.ChargeSecondary, "Charges when needs reload and needs charge");

        yield return (0, 0, false, false, false, null, "Does nothing when needs to charge, but cannot reload or attack");

        yield return (10, 1, false, true, true, BattleAction.DisarmSecondary, "Disarmed when ammo remaining and disarmed");
        yield return (0, 1, true, true, true, BattleAction.DisarmSecondary, "Disarmed when want to reload remaining and disarmed");
    }

    private static IEnumerable<(int currentMagazineAmmo, BattleAction? expected, string testName)> TemporaryWeaponStrategy_BasedOnStatus_PerformsAction_TestData()
    {
        yield return (1, BattleAction.UseTemporary, "Use temp when ammo available.");
        yield return (0, null, "Skip temp when no ammo.");
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
}*/