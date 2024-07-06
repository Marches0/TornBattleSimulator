using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

public interface IModifier
{
    ModifierLifespanDescription Lifespan { get; }

    bool RequiresDamageToApply { get; }

    ModifierTarget Target { get; }

    ModifierApplication AppliesAt { get; }

    public ModifierType Effect { get; }
}