using TornBattleSimulator.Core.Thunderdome.Damage;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

public interface IOwnedLifespan : IModifier
{
    bool Expired(AttackResult? attack);
}