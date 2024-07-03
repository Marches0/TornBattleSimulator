namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Modifiers;

public interface IStatsModifier : IModifier
{
    float GetStrengthModifier();

    float GetDefenceModifier();

    float GetSpeedModifier();

    float GetDexterityModifier();
}