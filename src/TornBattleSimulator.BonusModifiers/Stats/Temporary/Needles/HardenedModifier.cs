using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.BonusModifiers.Stats.Temporary.Needles;

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

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}