using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

public interface IOwnedLifespan : IModifier
{
    bool Expired(PlayerContext owner, AttackResult? attack);
}