using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.Actions;

[TestFixture]
public class UseWeaponTests
{
    [TestCaseSource(nameof(UseWeaponAction_UsesCorrectWeapon_TestCases))]
    public void UseWeaponAction_UsesCorrectWeapon((IAction action, EquippedWeapons weapons, WeaponContext expected, IWeaponUsage weaponUsage) testData)
    {
        var attacker = new PlayerContextBuilder().WithWeapons(testData.weapons).Build();
        var defender = new PlayerContextBuilder().Build();

        testData.action.PerformAction(new ThunderdomeContextBuilder().WithParticipants(attacker, defender).Build(), attacker, defender);

        A.CallTo(() => testData.weaponUsage.UseWeapon(A<ThunderdomeContext>._, A<PlayerContext>._, A<PlayerContext>._, testData.expected))
            .MustHaveHappenedOnceExactly();
    }

    private static IEnumerable<(IAction action, EquippedWeapons weapons, WeaponContext expected, IWeaponUsage weaponUsage)> UseWeaponAction_UsesCorrectWeapon_TestCases()
    {
        using AutoFake autoFake = new();
        EquippedWeapons weapons = new EquippedWeapons(
            new WeaponContext(new Weapon(), WeaponType.Primary, new List<PotentialModifier>(), new List<IModifier>()),
            new WeaponContext(new Weapon(), WeaponType.Secondary, new List<PotentialModifier>(), new List<IModifier>()),
            new WeaponContext(new Weapon(), WeaponType.Melee, new List<PotentialModifier>(), new List<IModifier>()),
            new WeaponContext(new Weapon(), WeaponType.Temporary, new List<PotentialModifier>(), new List<IModifier>())
        );

        IWeaponUsage usage = autoFake.Resolve<IWeaponUsage>();
        yield return (new AttackPrimaryAction(usage), weapons, weapons.Primary!, usage);

        usage = autoFake.Resolve<IWeaponUsage>();
        yield return (new AttackSecondaryAction(usage), weapons, weapons.Secondary!, usage);

        usage = autoFake.Resolve<IWeaponUsage>();
        yield return (new AttackMeleeAction(usage), weapons, weapons.Melee!, usage);

        usage = autoFake.Resolve<IWeaponUsage>();
        yield return (new UseTemporaryAction(usage), weapons, weapons.Temporary!, usage);
    }
}