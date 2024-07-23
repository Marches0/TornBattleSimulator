using TornBattleSimulator.Shared.Thunderdome.Damage;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Shared.Thunderdome.Modifiers.Health;

public interface IHealthModifier : IModifier
{
    int GetHealthMod(PlayerContext target, DamageResult? damage);
}