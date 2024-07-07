namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats;

public interface IStatsModifier : IModifier
{
    // put these into their own thing?
    float GetStrengthModifier();

    float GetDefenceModifier();

    float GetSpeedModifier();

    float GetDexterityModifier();
}