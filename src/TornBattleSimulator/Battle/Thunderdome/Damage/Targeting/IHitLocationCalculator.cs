using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;

public interface IHitLocationCalculator
{
    BodyPart GetHitLocation(AttackContext attack);
}