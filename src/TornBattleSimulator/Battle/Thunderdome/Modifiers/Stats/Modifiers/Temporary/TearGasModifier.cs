namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Modifiers.Temporary;

public class TearGasModifier : IStatsModifier
{
    private const float Modifier = 1 / 3;

    public float GetDefenceModifier() => 1;

    public float GetDexterityModifier() => Modifier;

    public float GetSpeedModifier() => 1;

    public float GetStrengthModifier() => 1;

    public float TimeRemainingSeconds { get; set; } = 120;

    public bool RequiresDamageToApply { get; } = false;
}