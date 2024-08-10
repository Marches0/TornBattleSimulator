using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;

public class DamageTargeter : IDamageTargeter
{
    private readonly IHitLocationCalculator _hitLocationCalculator;
    private readonly IHitArmourCalculator _hitArmourCalculator;

    public DamageTargeter(
        IHitLocationCalculator hitLocationCalculator,
        IHitArmourCalculator hitArmourCalculator)
    {
        _hitLocationCalculator = hitLocationCalculator;
        _hitArmourCalculator = hitArmourCalculator;
    }

    public HitLocation GetDamageTarget(AttackContext attack)
    {
        BodyPart struckPart = _hitLocationCalculator.GetHitLocation(attack);
        ArmourContext? armourStruck = _hitArmourCalculator.GetHitArmour(attack, struckPart);
        return new(struckPart, armourStruck);
    }
}