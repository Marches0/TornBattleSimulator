using Autofac.Extras.FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Battle.Thunderdome.Player.Armours;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Options;

namespace TornBattleSimulator.UnitTests.Thunderdome.Player;

[TestFixture]
public class ArmourFactoryTests
{
    //[Test]
    public void Create_WhenHasSetBonus_GetsAdditionalModifier()
    {
        // redo this
        // Arrange
        List<ArmourCoverageOption> armourCoverage = [ new ArmourCoverageOption() { Name = "test", Coverage = new() }];

        RootConfig rootConfig = new()
        {
            ArmourSetBonuses = new()
            {
                { ModifierType.Impregnable, 20 }
            }
        };

        Armour standardPiece = new Armour()
        {
            Name = "test",
            Rating = 10,
            Modifiers = [new ModifierDescription() { Percent = 1, Type = ModifierType.Impregnable }]
        };

        ArmourSet armourSet = new()
        {
            Helmet = standardPiece,
            Body = standardPiece,
            Pants = standardPiece,
            Gloves = standardPiece,
            Boots = standardPiece
        };

        using AutoFake autofake = new();
        autofake.Provide(armourCoverage);
        autofake.Provide(rootConfig);

        ArmourFactory armourFactory = autofake.Resolve<ArmourFactory>();

        // Act
        var armourContext = armourFactory.Create(armourSet);

        // Assert
        using (new AssertionScope())
        {
            armourContext.PotentialModifiers.Count().Should()
                .Be(armourContext.Armour.Count + 1);

            // Should just use IModifierFactory to return a test modifier that we
            // don't have to jump through hoops for.
            AttackContext attack = new AttackContextBuilder()
                .WithWeapon(new WeaponContextBuilder().OfType(WeaponType.Melee).Build())
                .Build();

            armourContext.PotentialModifiers.Should()
                .Contain(pm => ((ImpregnableModifier)pm.Modifier).GetDamageModifier(
                    attack,
                    new HitLocation(0, null)) == 0.8);
        }
    }
}