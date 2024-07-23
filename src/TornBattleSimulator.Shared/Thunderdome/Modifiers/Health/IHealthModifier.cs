using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Health;

public interface IHealthModifier : IModifier
{
    int GetHealthMod(PlayerContext target, DamageResult? damage);
}