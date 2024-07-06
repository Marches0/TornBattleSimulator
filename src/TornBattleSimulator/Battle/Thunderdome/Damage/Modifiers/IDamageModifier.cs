namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public interface IDamageModifier
{
    DamageModifierResult GetDamageModifier(
        PlayerContext active,
        PlayerContext other);
}