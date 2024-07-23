using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

public interface IModifier
{
    ModifierLifespanDescription Lifespan { get; }

    bool RequiresDamageToApply { get; }

    ModifierTarget Target { get; }

    ModifierApplication AppliesAt { get; }

    public ModifierType Effect { get; }
}