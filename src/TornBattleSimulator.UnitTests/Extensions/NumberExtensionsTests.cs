using FluentAssertions;
using TornBattleSimulator.Core.Extensions;

namespace TornBattleSimulator.UnitTests.Extensions;

[TestFixture]
public class NumberExtensionsTests
{
    [TestCase(1UL, "1")]
    [TestCase(1_000UL, "1000")]
    [TestCase(5_000UL, "5000")]
    [TestCase(10_000UL, "10k")]
    [TestCase(39_999UL, "40k")]
    [TestCase(50_000UL, "50k")]
    [TestCase(1_000_000UL, "1.0m")]
    [TestCase(5_000_000UL, "5.0m")]
    [TestCase(10_000_000UL, "10.0m")]
    [TestCase(1_000_000_000UL, "1.0b")]
    [TestCase(5_000_000_000UL, "5.0b")]
    [TestCase(1_000_000_000_000UL, "1.0t")]
    [TestCase(5_000_000_000_000UL, "5.0t")]
    public void Number_ToShortString_ReturnsExpected(ulong number, string expected)
    {
        number.ToSimpleString().Should().Be(expected);
    }
}