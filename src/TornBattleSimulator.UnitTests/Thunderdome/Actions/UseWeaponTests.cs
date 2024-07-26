using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.Actions;

[TestFixture]
public class UseWeaponTests
{
    [TestCaseSource(nameof(UseWeaponAction_UsesCorrectWeapon_TestCases))]
    public void UseWeaponAction_UsesCorrectWeapon((IAction action, PlayerContext attacker, WeaponContext expected, IWeaponUsage weaponUsage) testData)
    {
        var defender = new PlayerContextBuilder().Build();

        testData.action.PerformAction(new ThunderdomeContextBuilder().WithParticipants(testData.attacker, defender).Build(), testData.attacker, defender);

        A.CallTo(() => testData.weaponUsage.UseWeapon(A<ThunderdomeContext>._, A<PlayerContext>._, A<PlayerContext>._, testData.expected))
            .MustHaveHappenedOnceExactly();
    }

    private static IEnumerable<(
        IAction action,
        PlayerContext attacker,
        WeaponContext expected,
        IWeaponUsage weaponUsage
    )> UseWeaponAction_UsesCorrectWeapon_TestCases()
    {
        using AutoFake autoFake = new();
        EquippedWeapons weapons = new EquippedWeapons(
            new WeaponContext(new Weapon(), WeaponType.Primary, new List<PotentialModifier>()),
            new WeaponContext(new Weapon(), WeaponType.Secondary, new List<PotentialModifier>()),
            new WeaponContext(new Weapon(), WeaponType.Melee, new List<PotentialModifier>()),
            new WeaponContext(new Weapon(), WeaponType.Temporary, new List<PotentialModifier>())
        );

        IWeaponUsage usage = autoFake.Resolve<IWeaponUsage>();
        yield return (new AttackPrimaryAction(usage), new PlayerContextBuilder().WithWeapons(weapons).Build(), weapons.Primary!, usage);

        usage = autoFake.Resolve<IWeaponUsage>();
        yield return (new AttackSecondaryAction(usage), new PlayerContextBuilder().WithWeapons(weapons).Build(), weapons.Secondary!, usage);

        usage = autoFake.Resolve<IWeaponUsage>();
        yield return (new AttackMeleeAction(usage), new PlayerContextBuilder().WithWeapons(weapons).Build(), weapons.Melee!, usage);

        usage = autoFake.Resolve<IWeaponUsage>();
        yield return (new UseTemporaryAction(usage), new PlayerContextBuilder().WithWeapons(weapons).Build(), weapons.Temporary!, usage);
    }
}