using TornBattleSimulator.Core.Thunderdome;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;

public interface IDamageTargeter
{
    HitLocation GetDamageTarget(AttackContext attack); 
}