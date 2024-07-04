namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Modifiers;

public interface IStatsModifier : IModifier
{
    // put these into their own thing?
    float GetStrengthModifier();

    float GetDefenceModifier();

    float GetSpeedModifier();

    float GetDexterityModifier();
}