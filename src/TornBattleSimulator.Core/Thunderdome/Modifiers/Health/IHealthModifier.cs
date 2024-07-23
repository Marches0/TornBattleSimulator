using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Health;

public interface IHealthModifier : IModifier
{
    int GetHealthMod(PlayerContext target, DamageResult? damage);
}