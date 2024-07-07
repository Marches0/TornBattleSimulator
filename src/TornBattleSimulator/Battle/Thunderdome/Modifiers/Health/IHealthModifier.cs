using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Health;

public interface IHealthModifier : IModifier
{
    int GetHealthMod(PlayerContext target, DamageResult? damage);
}