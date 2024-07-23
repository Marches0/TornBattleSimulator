using TornBattleSimulator.Shared.Build.Equipment;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Shared.Thunderdome.Modifiers;

public interface IModifier
{
    ModifierLifespanDescription Lifespan { get; }

    bool RequiresDamageToApply { get; }

    ModifierTarget Target { get; }

    ModifierApplication AppliesAt { get; }

    public ModifierType Effect { get; }
}