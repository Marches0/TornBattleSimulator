using FluentAssertions;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Events;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;

[TestFixture]
public class BlindfireModifierTests
{
    [Test]
    public void BlindfireModifier_MakeAttack_AttacksWhileAmmoRemaining()
    {
        // Arrange
        PlayerContext active = new PlayerContextBuilder().Build();
        PlayerContext other = new PlayerContextBuilder().Build();
        ThunderdomeContext context = new ThunderdomeContextBuilder().WithParticipants(active, other).Build();

        // 10 attacks
        WeaponContext weapon = new WeaponContextBuilder().WithAmmo(1, 20).Build();

        int expectedAttacks = 10;
        int attacksMade = 0;
        Func<List<ThunderdomeEvent>> attackAction = () =>
        {
            if (++attacksMade == expectedAttacks)
            {
                weapon.Ammo!.MagazineAmmoRemaining = 0;
            }

            return [];
        };

        BlindfireModifier blindfire = new BlindfireModifier();

        // Act
        blindfire.MakeAttack(context, active, other, weapon, attackAction);

        // Assert
        attacksMade.Should().Be(expectedAttacks);
    }
}