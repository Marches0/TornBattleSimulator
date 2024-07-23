using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Temporary;

public class GassedModifier : IStatsModifier
{
    public float GetDefenceModifier() => 1;

    public float GetDexterityModifier() => 1 / 3f;

    public float GetSpeedModifier() => 1;

    public float GetStrengthModifier() => 1;

    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(120);

    public bool RequiresDamageToApply { get; } = false;

    public ModifierTarget Target { get; } = ModifierTarget.Other;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Gassed;

    public StatModificationType Type => StatModificationType.Multiplicative;
}