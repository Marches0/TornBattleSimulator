using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;

namespace TornBattleSimulator.UnitTests.Thunderdome.Test.Modifiers;

public class TestConditionalModifier : BaseTestModifier, IConditionalModifier
{
    private readonly bool _willActivate;

    public TestConditionalModifier(bool willActivate)
    {
        _willActivate = willActivate;
    }

    public override ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Chance;

    public bool CanActivate(AttackContext attack) => _willActivate;
}