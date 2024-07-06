using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;
public class ArmourDamageModifier : IDamageModifier
{
    public DamageModifierResult GetDamageModifier(PlayerContext active, PlayerContext other, WeaponContext weapon, DamageContext damageContext)
    {
        // - find the appropriate piece of armour based on body part
        // - roll for "does it cover"

        // or sum the coverage of each body part with the armour? since there is some overlap.

        throw new NotImplementedException();
    }
}