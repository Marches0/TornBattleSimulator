using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Player.Armours;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Options;

namespace TornBattleSimulator.UnitTests.Thunderdome.Player;

[TestFixture]
public class ArmourFactoryTests
{
    [TestCaseSource(nameof(Create_BasedOnBonuses_MayApplySetBonus_TestCases))]
    public void Create_BasedOnBonuses_MayApplySetBonus((
        ArmourSet armourSet,
        double expectedBonus,
        int armourPieceCount,
        string testName) testData)
    {
        // Arrange
        List<ArmourCoverageOption> armourCoverage = [ new ArmourCoverageOption() { Name = "test", Coverage = new() }];

        RootConfig rootConfig = new()
        {
            ArmourSetBonuses = new()
            {
                { ModifierType.Impregnable, 20 }
            }
        };

        IModifierFactory modifierFactory = A.Fake<IModifierFactory>();

        using AutoFake autofake = new();
        autofake.Provide(armourCoverage);
        autofake.Provide(rootConfig);
        autofake.Provide(modifierFactory);

        ArmourFactory armourFactory = autofake.Resolve<ArmourFactory>();

        // Act
        var armourContext = armourFactory.Create(testData.armourSet);

        // Assert
        using (new AssertionScope())
        {
            A.CallTo(() => modifierFactory.GetModifier(ModifierType.Impregnable, testData.expectedBonus))
                .MustHaveHappened(testData.armourPieceCount, Times.Exactly);
        }
    }

    private static IEnumerable<(
        ArmourSet armourSet,
        double expectedBonus,
        int armourPieceCount,
        string testName)> Create_BasedOnBonuses_MayApplySetBonus_TestCases()
    {
        double regularPercent = 10;
        double bonusPercent = 30;

        Armour standardPiece = new Armour()
        {
            Name = "test",
            Rating = 2,
            Modifiers = [new ModifierDescription() { Percent = regularPercent, Type = ModifierType.Impregnable }]
        };

        yield return (
            new ArmourSet()
            {
                Helmet = standardPiece,
                Body = standardPiece,
                Pants = standardPiece,
                Gloves = standardPiece,
                Boots = standardPiece
            },
            bonusPercent,
            5,
            "All pieces with effect gives bonus"
        );

        yield return (
            new ArmourSet()
            {
                Helmet = standardPiece,
                Body = standardPiece,
                Pants = standardPiece,
                Gloves = standardPiece
            },
            regularPercent,
            4,
            "Fewer pieces with effect gives no bonus"
        );
    }
}