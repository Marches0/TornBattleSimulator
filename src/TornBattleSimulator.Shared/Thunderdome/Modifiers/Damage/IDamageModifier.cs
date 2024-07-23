using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;

public interface IDamageModifier
{
    DamageModifierResult GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext);
}