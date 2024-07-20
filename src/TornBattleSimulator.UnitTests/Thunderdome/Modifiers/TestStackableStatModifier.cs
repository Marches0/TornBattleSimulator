using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stacking;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;

namespace TornBattleSimulator.UnitTests.Thunderdome.Modifiers;

internal class TestStackableStatModifier : IStackableStatModifier
{
    private readonly float _strMod;
    private readonly float _defMod;
    private readonly float _spdMod;
    private readonly float _dexMod;
    private readonly int _maxStacks;

    public TestStackableStatModifier(
        float strMod,
        float defMod,
        float spdMod,
        float dexMod,
        int maxStacks)
    {
        _strMod = strMod;
        _defMod = defMod;
        _spdMod = spdMod;
        _dexMod = dexMod;
        _maxStacks = maxStacks;
    }

    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => ModifierTarget.Self;

    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Demoralized;

    public int MaxStacks => _maxStacks;

    public float GetDefenceModifier() => _defMod;

    public float GetDexterityModifier() => _dexMod;

    public float GetSpeedModifier() => _spdMod;

    public float GetStrengthModifier() => _strMod;
}