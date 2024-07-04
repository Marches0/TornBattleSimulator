using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Modifiers.Temporary;

public class TearGasModifier : IStatsModifier
{
    private const float Modifier = 1 / 3f;

    public float GetDefenceModifier() => 1;

    public float GetDexterityModifier() => Modifier;

    public float GetSpeedModifier() => 1;

    public float GetStrengthModifier() => 1;

    public ModifierLifespanDescription Lifespan { get; } = new ModifierLifespanDescription(ModifierLifespanType.Temporal, 120);

    public bool RequiresDamageToApply { get; } = false;

    public ModifierTarget Target { get; } = ModifierTarget.Other;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    public WeaponModifierType Effect => WeaponModifierType.Gassed;
}