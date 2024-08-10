using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;

public class HitLocation
{
    public HitLocation(
        BodyPart bodyPartStruck,
        ArmourContext? armourStruck)
    {
        BodyPartStruck = bodyPartStruck;
        ArmourStruck = armourStruck;
    }

    public BodyPart BodyPartStruck { get; }
    public ArmourContext? ArmourStruck { get; }
}