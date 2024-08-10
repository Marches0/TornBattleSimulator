using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;
using TornBattleSimulator.Core.Thunderdome;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;

public interface IHitArmourCalculator
{
    ArmourContext? GetHitArmour(
        AttackContext attack,
        BodyPart struckPart);
}