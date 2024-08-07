using FluentAssertions;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.BonusModifiers;

[TestFixture]
public class SprayModifierTests
{
    [TestCase(10, 10, true)]
    [TestCase(9, 10, false)]
    public void CanActivate_BasedOnMagazineAmmo(int currentAmmo, int maxAmmo, bool activates)
    {
        WeaponContext weapon = new WeaponContextBuilder()
            .WithAmmo(1, maxAmmo)
            .Build();

        weapon.Ammo.MagazineAmmoRemaining = currentAmmo;

        SprayModifier spray = new SprayModifier();

        spray.CanActivate(new Core.Thunderdome.AttackContext(
            new ThunderdomeContextBuilder().Build(),
            new PlayerContextBuilder().Build(),
            new PlayerContextBuilder().Build(),
            weapon,
            null)
        ).Should().Be(activates);
    }
}