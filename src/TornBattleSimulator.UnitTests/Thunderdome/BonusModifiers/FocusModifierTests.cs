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
            new PlayerContextBuilder().Build(),
            new PlayerContextBuilder().Build(),
            new AttackResult(isHit, 1, new DamageResult(1, 0, 0))
            ).Should().Be(activates);
    }

    [TestCase(true)]
    public void PerformAction_BasedOnHit_RemovesFocus(bool isHit)
    {
        // Arrange
        FocusModifier focus = new FocusModifier(1);
        WeaponContext weapon = new WeaponContextBuilder()
            .WithModifier(focus)
            .WithModifier(focus)
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
        List<ThunderdomeEvent> events = focus.PerformAction(attack);

        // Assert
        using (new AssertionScope())
        {
            if (isHit)
            {
                events.Should().AllSatisfy(evt => evt.Type.Should().Be(ThunderdomeEventType.EffectEnd));
                events.Should().HaveCount(2);
                weapon.Modifiers.Active.Should().BeEmpty();
            }
            else
            {
                events.Should().BeEmpty();
                weapon.Modifiers.Active.Should().HaveCount(2);
            }
        }
    }
}