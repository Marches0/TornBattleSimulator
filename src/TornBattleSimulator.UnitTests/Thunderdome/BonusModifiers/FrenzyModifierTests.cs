using FluentAssertions;
using TornBattleSimulator.BonusModifiers.Accuracy;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.BonusModifiers;

[TestFixture]
public class FrenzyModifierTests
{
    [TestCase(true, true)]
    [TestCase(false, false)]
    public void CanActivate_BasedOnHit(bool isHit, bool shouldActivate)
    {
        //new AttackResult(isHit, 1, new DamageResult(1, 0, 0))

        new FrenzyModifier(1)
            .CanActivate(
                new AttackContext(
                    new ThunderdomeContextBuilder().Build(),
                    new PlayerContextBuilder().Build(),
                    new PlayerContextBuilder().Build(),
                    new WeaponContextBuilder().Build(),
                    new AttackResult(isHit, 1, new DamageResult(1, 0, 0))
                )
            ).Should().Be(shouldActivate);
    }

    [TestCase(true, false)]
    [TestCase(false, true)]
    public void Expired_BasedOnHit(bool isHit, bool expired)
    {
        // Arrange
        FrenzyModifier frenzy = new FrenzyModifier(1);
        WeaponContext weapon = new WeaponContextBuilder()
            .WithModifier(frenzy)
            .WithModifier(frenzy)
            .Build();

        PlayerContext active = new PlayerContextBuilder()
            .WithPrimary(weapon)
            .Build();

        PlayerContext other = new PlayerContextBuilder().Build();
        AttackContext attack = new AttackContext(
            new ThunderdomeContextBuilder().WithParticipants(active, other).Build(),
            active,
            other,
            weapon,
            new AttackResult(isHit, 1, new DamageResult(1, 0, 0)));

        // Act
        frenzy.Expired(attack).Should().Be(expired);
    }
}