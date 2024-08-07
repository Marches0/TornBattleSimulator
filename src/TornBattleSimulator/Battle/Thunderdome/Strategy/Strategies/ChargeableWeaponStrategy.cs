using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public abstract class ChargeableWeaponStrategy
{
    protected bool NeedsCharge(WeaponContext weapon) => weapon.Modifiers.ChargeModifiers.Any(m => !m.Charged);
    protected bool Disarmed(WeaponContext weapon) => weapon.Modifiers.Active.OfType<DisarmModifier>().Any();
}