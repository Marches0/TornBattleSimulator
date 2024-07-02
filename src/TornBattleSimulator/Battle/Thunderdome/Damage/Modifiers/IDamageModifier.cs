namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public interface IDamageModifier
{
    double GetDamageModifier(
        PlayerContext active,
        PlayerContext other);
}