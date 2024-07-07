using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Temporary.Needles;

// also a health mod!!
// 25% heal TODOOO
public class HardenedModifier : IStatsModifier
{
    public float GetDefenceModifier() => 4;

    public float GetDexterityModifier() => 1;

    public float GetSpeedModifier() => 1;

    public float GetStrengthModifier() => 1;

    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(120);

    public bool RequiresDamageToApply { get; } = false;

    public ModifierTarget Target { get; } = ModifierTarget.Self;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Hardened;
}