namespace TornBattleSimulator.Battle.Thunderdome.Stats.Modifiers;

public interface IStatsModifier
{
    float GetStrengthModifier();

    float GetDefenceModifier();

    float GetSpeedModifier();

    float GetDexterityModifier();

    float TimeRemainingSeconds { get; set; }
}