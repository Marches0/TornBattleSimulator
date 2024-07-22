using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public abstract class ChargeableWeaponStrategy
{
    public bool NeedsCharge(WeaponContext weapon) => weapon.ChargedModifiers.Any(m => !m.Charged);
}