namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public interface IDamageModifier
{
    double GetDamageModifier(PlayerContext attacker, PlayerContext defender);
}