using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.BonusModifiers.Accuracy;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.BonusModifiers;

[TestFixture]
public class FocusModifierTests
{
    [TestCase(true, false)]
    [TestCase(false, true)]
    public void CanActivate_BasedOnHit(bool isHit, bool activates)
    {
        new FocusModifier(1)
            .CanActivate(
            new AttackContext(
                new ThunderdomeContextBuilder().Build(),
                new PlayerContextBuilder().Build(),
                new PlayerContextBuilder().Build(),
                new WeaponContextBuilder().Build(),
                new AttackResult(isHit, 1, new DamageResult(1, 0, 0)))
            ).Should().Be(activates);
    }

    [TestCase(true, true)]
    [TestCase(false, false)]
    public void Expired_BasedOnHit(bool isHit, bool expired)
    {
        // Arrange
        FocusModifier focus = new FocusModifier(1);

        // Act
        focus.Expired(new AttackResult(isHit, 1, new DamageResult(1, 0, 0))).Should().Be(expired);
    }
}