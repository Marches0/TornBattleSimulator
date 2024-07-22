using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Temporary.Needles;

public class HardenedModifier : IStatsModifier, IHealthModifier
{
    public float GetDefenceModifier() => 4;

    public float GetDexterityModifier() => 1;

    public float GetSpeedModifier() => 1;

    public float GetStrengthModifier() => 1;

    public int GetHealthMod(PlayerContext target, DamageResult? damage) => (int)(target.Health.MaxHealth * 0.25);

    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(120);

    public bool RequiresDamageToApply { get; } = false;

    public ModifierTarget Target { get; } = ModifierTarget.Self;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Hardened;

    public StatModificationType Type => StatModificationType.Additive;
}