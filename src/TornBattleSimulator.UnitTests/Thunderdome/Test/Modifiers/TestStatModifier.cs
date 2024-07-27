﻿using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

internal class TestStatModifier : IStatsModifier
{
    private readonly float _strengthModifier;
    private readonly float _defenceModifier;
    private readonly float _speedModifier;
    private readonly float _dexterityModifier;

    public TestStatModifier(
        float strengthModifier,
        float defenceModifier,
        float speedModifier,
        float dexterityModifier,
        StatModificationType type)
    {
        _strengthModifier = strengthModifier;
        _defenceModifier = defenceModifier;
        _speedModifier = speedModifier;
        _dexterityModifier = dexterityModifier;
        Type = type;
    }

    public ModifierLifespanDescription Lifespan => throw new NotImplementedException();

    public bool RequiresDamageToApply => throw new NotImplementedException();

    public ModifierTarget Target => throw new NotImplementedException();

    public ModifierApplication AppliesAt => throw new NotImplementedException();

    public ModifierType Effect => throw new NotImplementedException();

    public float GetDefenceModifier() => _defenceModifier;

    public float GetDexterityModifier() => _dexterityModifier;

    public float GetSpeedModifier() => _speedModifier;

    public float GetStrengthModifier() => _strengthModifier;

    public StatModificationType Type { get; }

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}