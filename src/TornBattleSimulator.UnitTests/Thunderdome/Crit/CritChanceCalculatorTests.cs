using FluentAssertions;
using TornBattleSimulator.BonusModifiers.CritChance;
using TornBattleSimulator.Core.Thunderdome.Damage.Critical;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.Crit;

[TestFixture]
public class CritChanceCalculatorTests
{
    [TestCaseSource(nameof(GetCritChance_CalculatesWithModifiers_TestCases))]
    public void GetCritChance_CalculatesWithModifiers((
        List<IModifier> playerModifiers,
        List<IModifier> weaponModifiers,
        double expected
        ) testData)
    {
        // Arrange
        PlayerContext active = new PlayerContextBuilder()
            .Build();

        foreach (var modifier in testData.playerModifiers)
        {
            active.Modifiers.AddModifier(modifier, null);
        }

        WeaponContext weapon = new WeaponContextBuilder()
            .Build();

        foreach (var modifier in testData.weaponModifiers)
        {
            weapon.Modifiers.AddModifier(modifier, null);
        }

        // Act
        var critChance = new CritChanceCalculator()
            .GetCritChance(active, new PlayerContextBuilder().Build(), weapon);

        // Assert
        critChance.Should().Be(testData.expected);
    }

    private static IEnumerable<(
        List<IModifier> playerModifiers,
        List<IModifier> weaponModifiers,
        double expected
        )> GetCritChance_CalculatesWithModifiers_TestCases()
    {
        yield return ([], [], 0.12);
        yield return ([], [ new ExposeModifier(0.1) ], 0.22);
        yield return ([], [ new ExposeModifier(0.2), new ExposeModifier(0.2) ], 0.52);
    }
}