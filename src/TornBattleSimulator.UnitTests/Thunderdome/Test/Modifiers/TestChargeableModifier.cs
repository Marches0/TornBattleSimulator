﻿using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Charge;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

internal class TestChargeableModifier : IChargeableModifier
{
    public TestChargeableModifier(bool startsCharged)
    {
        StartsCharged = startsCharged;
    }

    public bool StartsCharged { get; }

    public ModifierLifespanDescription Lifespan => throw new NotImplementedException();

    public bool RequiresDamageToApply => throw new NotImplementedException();

    public ModifierTarget Target => throw new NotImplementedException();

    public ModifierApplication AppliesAt => throw new NotImplementedException();

    public ModifierType Effect => throw new NotImplementedException();

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.None;
}