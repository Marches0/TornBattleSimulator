using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

public interface IModifier
{
    float TimeRemainingSeconds { get; set; }

    bool RequiresDamageToApply { get; }

    ModifierTarget Target { get; }

    ModifierApplication AppliesAt { get; }

    public WeaponModifierType Effect { get; }
}