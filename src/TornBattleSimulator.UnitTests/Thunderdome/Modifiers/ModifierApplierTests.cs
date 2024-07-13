using FakeItEasy;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.UnitTests.Chance;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;

[TestFixture]
public class ModifierApplierTests
{
    [Test]
    public void ModifierApplier_ForHealingPostActionModifier_ImmediatelyHealsCorrectAmount()
    {
        PlayerContext active = new PlayerContextBuilder().Build();
        PlayerContext other = new PlayerContextBuilder().Build();
        ThunderdomeContext context = new ThunderdomeContextBuilder().WithParticipants(active, other).Build();

        IHealthModifier healthMod = A.Fake<IHealthModifier>();
        A.CallTo(() => healthMod.AppliesAt)
            .Returns(ModifierApplication.AfterAction);
        A.CallTo(() => healthMod.Lifespan)
            .Returns(ModifierLifespanDescription.Turns(1));

        ModifierApplier modifierApplier = new(FixedChanceSource.AlwaysSucceeds);

        modifierApplier.ApplyPostActionModifiers(
            context,
            active,
            other,
            new List<PotentialModifier>() { new PotentialModifier(healthMod, 1) },
            new DamageResult(100, 0, 0),
            false);

        A.CallTo(() => healthMod.GetHealthMod(A<PlayerContext>._, A<DamageResult>._))
            .MustHaveHappenedOnceExactly();
    }
}