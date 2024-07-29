using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

internal class TestStatModifier : BaseTestModifier, IStatsModifier
{
    private readonly double _strengthModifier;
    private readonly double _defenceModifier;
    private readonly double _speedModifier;
    private readonly double _dexterityModifier;

    public TestStatModifier(
        double strengthModifier,
        double defenceModifier,
        double speedModifier,
        double dexterityModifier,
        StatModificationType type)
    {
        _strengthModifier = strengthModifier;
        _defenceModifier = defenceModifier;
        _speedModifier = speedModifier;
        _dexterityModifier = dexterityModifier;
        Type = type;
    }

    public double GetDefenceModifier() => _defenceModifier;

    public double GetDexterityModifier() => _dexterityModifier;

    public double GetSpeedModifier() => _speedModifier;

    public double GetStrengthModifier() => _strengthModifier;

    public StatModificationType Type { get; }
}