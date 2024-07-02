using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Strategy;

namespace TornBattleSimulator.UnitTests.Thunderdome.Damage.Modifiers;

[TestFixture]
public class WeaponDamageModifierTests
{
    private readonly WeaponDamageModifier _weaponDamageModifier = new();

    private const double PrimaryDamage = 20;
    private const double SecondaryDamage = 40;
    private const double MeleeDamage = 60;

    [TestCaseSource(nameof(WeaponDamageModifier_BasedOnCurrentAction_ChoosesAppropriateWeapon_TestCases))]
    public void WeaponDamageModifier_BasedOnCurrentAction_ChoosesAppropriateWeapon((PlayerContext player, BattleAction action, double expectedDamage) testData)
    {
        // Verify the test data is sound, since we know which weapon we chose based on 
        // the damage output - if they're the same, it's ambiguous.
        // Maybe the weapon selection should be behind some kind of interface? Maybe one day.
        Assert.True(testData.player.Build.Primary.Damage != testData.player.Build.Secondary.Damage);
        Assert.True(testData.player.Build.Primary.Damage != testData.player.Build.Melee.Damage);
        Assert.True(testData.player.Build.Secondary.Damage != testData.player.Build.Melee.Damage);
    }

    private static IEnumerable<(PlayerContext player, BattleAction action, double expectedDamage)> WeaponDamageModifier_BasedOnCurrentAction_ChoosesAppropriateWeapon_TestCases()
    {
        yield return (GetArmedPlayer(), BattleAction.AttackPrimary, PrimaryDamage / 10);
        yield return (GetArmedPlayer(), BattleAction.AttackSecondary, SecondaryDamage / 10);
        yield return (GetArmedPlayer(), BattleAction.AttackMelee, MeleeDamage / 10);
    }

    private static PlayerContext GetArmedPlayer()
    {
        return new PlayerContextBuilder()
            .WithPrimary(new Weapon() { Damage = PrimaryDamage, Ammo = new Ammo() })
            .WithSecondary(new Weapon() { Damage = SecondaryDamage, Ammo = new Ammo() })
            .WithMelee(new Weapon() { Damage = MeleeDamage })
            .Build();
    }
}