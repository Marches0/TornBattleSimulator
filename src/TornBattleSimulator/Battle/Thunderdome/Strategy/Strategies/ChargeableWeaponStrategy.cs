using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public abstract class ChargeableWeaponStrategy
{
    public bool NeedsCharge(WeaponContext weapon) => weapon.Modifiers.ChargeModifiers.Any(m => !m.Charged);
}