using TornBattleSimulator.Core.Thunderdome.Modifiers.CritChance;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome.Damage.Critical;

public class CritChanceCalculator : ICritChanceCalculator
{
    const double BaseCritChance = 0.12;

    public double GetCritChance(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        return active.Modifiers.Active.Concat(weapon.Modifiers.Active)
            .OfType<ICritChanceModifier>()
            .Aggregate(BaseCritChance, (total, modifier) => total + modifier.GetCritChanceModifier());
    }
}