﻿using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestConditionalModifier : BaseTestModifier, IConditionalModifier
{
    private readonly bool _willActivate;

    public TestConditionalModifier(bool willActivate)
    {
        _willActivate = willActivate;
    }

    public override ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;

    public bool CanActivate(PlayerContext active, PlayerContext other) => _willActivate;
}