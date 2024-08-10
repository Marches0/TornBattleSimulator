using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Options;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;

public interface IHitLocationCalculator
{
    BodyPartDamage GetHitLocation(AttackContext attack);
}