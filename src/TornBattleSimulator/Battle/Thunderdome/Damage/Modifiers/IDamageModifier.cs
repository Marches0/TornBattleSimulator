using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public interface IDamageModifier
{
    DamageModifierResult GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon);
}