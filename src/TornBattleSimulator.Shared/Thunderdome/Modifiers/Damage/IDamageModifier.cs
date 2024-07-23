using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public interface IDamageModifier
{
    DamageModifierResult GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext);
}