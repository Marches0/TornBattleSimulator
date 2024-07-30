using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stackable;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

internal class TestStackableStatModifier : IStackableStatModifier
{
    private readonly double _strMod;
    private readonly double _defMod;
    private readonly double _spdMod;
    private readonly double _dexMod;
    private readonly int _maxStacks;

    public TestStackableStatModifier(
        double strMod,
        double defMod,
        double spdMod,
        double dexMod,
        int maxStacks)
    {
        _strMod = strMod;
        _defMod = defMod;
        _spdMod = spdMod;
        _dexMod = dexMod;
        _maxStacks = maxStacks;
    }

    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    public bool RequiresDamageToApply { get; } = false;

    public ModifierTarget Target { get; } = ModifierTarget.Self;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    public ModifierType Effect { get; } = 0;

    public int MaxStacks => _maxStacks;

    public double GetDefenceModifier() => _defMod;

    public double GetDexterityModifier() => _dexMod;

    public double GetSpeedModifier() => _spdMod;

    public double GetStrengthModifier() => _strMod;

    public StatModificationType Type { get; } = StatModificationType.Multiplicative;

    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Chance;
}