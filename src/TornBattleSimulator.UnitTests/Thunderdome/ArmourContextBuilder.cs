using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class ArmourContextBuilder
{
    private IEnumerable<IModifier> _modifiers = Enumerable.Empty<IModifier>();

    public ArmourContextBuilder WithModifiers(IEnumerable<IModifier> modifiers)
    {
        _modifiers = modifiers;
        return this;
    }

    public ArmourContext Build()
    {
        ArmourContext armour = new(10, [], []);
        armour.Modifiers = new(null);

        foreach (var modifier in _modifiers)
        {
            armour.Modifiers.AddModifier(modifier, null);
        }

        return armour;
    }
}