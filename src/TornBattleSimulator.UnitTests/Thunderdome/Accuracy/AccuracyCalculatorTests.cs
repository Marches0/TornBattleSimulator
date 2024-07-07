using FakeItEasy;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Accuracy;
using TornBattleSimulator.Battle.Thunderdome.Accuracy.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome.Accuracy;

[TestFixture]
public class AccuracyCalculatorTests
{
    [Test]
    public void AccuracyCalculator_PassesStatAccuracyToWeaponAccuracy()
    {
        const double statAccuracy = 0.1234;

        var statModifier = A.Fake<ISpeedDexterityAccuracyModifier>();
        var accuracyModifier = A.Fake<IWeaponAccuracyModifier>();

        A.CallTo(() => statModifier.GetHitChance(A<PlayerContext>._, A<PlayerContext>._))
            .Returns(statAccuracy);

        var calc = new AccuracyCalculator(statModifier, accuracyModifier);

        var mod = calc.GetAccuracy(new PlayerContextBuilder().Build(), new PlayerContextBuilder().Build(), new WeaponContext(new Weapon(), WeaponType.Primary, new List<PotentialModifier>()));

        // Assert
        A.CallTo(() => accuracyModifier.GetHitChance(A<PlayerContext>._, A<PlayerContext>._, A<WeaponContext>._, statAccuracy))
            .MustHaveHappenedOnceExactly();
    }
}