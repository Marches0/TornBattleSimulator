using FluentAssertions;
using FluentAssertions.Execution;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Stackable;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;

[TestFixture]
public class StackableStatModifierContainerTests
{
    [TestCaseSource(nameof(GetModifier_WithIncreasingStacks_ModifiesAdditively_TestCases))]
    public void GetModifier_WithIncreasingStacks_ModifiesAdditively(
        (float strMod, float defMod, float spdMod, float dexMod, int stacks, float expectedStr, float expectedDef, float expectedSpd, float expectedDex) testData)
    {
        // Arrange
        TestStackableStatModifier modifier = new(testData.strMod, testData.defMod, testData.spdMod, testData.dexMod, 100);
        StackableStatModifierContainer container = new(modifier, new PlayerContextBuilder().Build());
        for (int i = 0; i < testData.stacks; i++)
        {
            container.AddStack();
        }

        // Act/Assert
        using (new AssertionScope())
        {
            container.GetStrengthModifier().Should().BeApproximately(testData.expectedStr, 0.0001f);
            container.GetDefenceModifier().Should().BeApproximately(testData.expectedDef, 0.0001f);
            container.GetSpeedModifier().Should().BeApproximately(testData.expectedSpd, 0.0001f);
            container.GetDexterityModifier().Should().BeApproximately(testData.expectedDex, 0.0001f);
        }
    }

    private static IEnumerable<(float strMod, float defMod, float spdMod, float dexMod, int stacks, float expectedStr, float expectedDef, float expectedSpd, float expectedDex)>
        GetModifier_WithIncreasingStacks_ModifiesAdditively_TestCases()
    {
        // Str  10%
        // Def  20%
        // Spd -10%
        // Dex -20%
        const float strMod = 1.1f;
        const float defMod = 1.2f;
        const float spdMod = 0.9f;
        const float dexMod = 0.8f;

        yield return (strMod, defMod, spdMod, dexMod, 0, 1f, 1f, 1f, 1f);
        yield return (strMod, defMod, spdMod, dexMod, 1, 1.1f, 1.2f, 0.9f, 0.8f);
        yield return (strMod, defMod, spdMod, dexMod, 2, 1.2f, 1.4f, 0.8f, 0.6f);
        yield return (strMod, defMod, spdMod, dexMod, 3, 1.3f, 1.6f, 0.7f, 0.4f);
        yield return (strMod, defMod, spdMod, dexMod, 4, 1.4f, 1.8f, 0.6f, 0.2f);
        yield return (strMod, defMod, spdMod, dexMod, 5, 1.5f, 2.0f, 0.5f, 0.0f);
    }

    [Test]
    public void AddStack_WhenAtMaxStacks_DoesNotAdd()
    {
        // Arrange
        int maxStacks = 2;
        TestStackableStatModifier modifier = new(1, 1, 1, 1, maxStacks);
        StackableStatModifierContainer container = new(modifier, new PlayerContextBuilder().Build());

        // Act / Assert
        container.AddStack().Should().BeTrue();
        container.AddStack().Should().BeTrue();
        container.AddStack().Should().BeFalse();
        container.Stacks.Should().Be(maxStacks);
    }
}