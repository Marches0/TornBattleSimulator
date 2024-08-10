using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;
using TornBattleSimulator.Options;

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
        BodyPartDamage struckPart = _hitLocationCalculator.GetHitLocation(attack);
        ArmourContext? armourStruck = _hitArmourCalculator.GetHitArmour(attack, struckPart.Part);
        return new(struckPart.Part, armourStruck);
    }
}