using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Modifiers.Temporary;

public class TearGasModifier : IStatsModifier
{
    private const float Modifier = 1 / 3f;

    public float GetDefenceModifier() => 1;

    public float GetDexterityModifier() => Modifier;

    public float GetSpeedModifier() => 1;

    public float GetStrengthModifier() => 1;

    public float TimeRemainingSeconds { get; set; } = 120;

    public bool RequiresDamageToApply { get; } = false;

    public ModifierTarget Target { get; } = ModifierTarget.Other;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    public WeaponModifierType Effect => WeaponModifierType.Gassed;
}