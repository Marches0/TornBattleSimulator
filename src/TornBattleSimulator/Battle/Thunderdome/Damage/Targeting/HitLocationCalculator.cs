using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Damage.Critical;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Options;
using TornBattleSimulator.Core.Thunderdome;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;

public class HitLocationCalculator : IHitLocationCalculator
{
    private readonly ICritChanceCalculator _critChanceCalculator;
    private readonly IChanceSource _modifierChanceSource;

    private readonly List<OptionChance<BodyPartDamage>> _criticalOptions;
    private readonly List<OptionChance<BodyPartDamage>> _regularOptions;

    public HitLocationCalculator(
        BodyModifierOptions bodyModifierOptions,
        ICritChanceCalculator critChanceCalculator,
        IChanceSource modifierChanceSource)
    {
        _criticalOptions = bodyModifierOptions.CriticalHits
            .Select(h => new OptionChance<BodyPartDamage>(h, h.Chance))
            .ToList();

        _regularOptions = bodyModifierOptions.RegularHits
            .Select(h => new OptionChance<BodyPartDamage>(h, h.Chance))
            .ToList();

        _critChanceCalculator = critChanceCalculator;
        _modifierChanceSource = modifierChanceSource;
    }

    public BodyPartDamage GetHitLocation(AttackContext attack)
    {
        if (attack.Weapon.Type == WeaponType.Temporary)
        {
            // Temps hit chest. Thanks Staphy!
            return _regularOptions.First(r => r.Option.Part == BodyPart.Chest).Option;
        }

        bool isCrit = _modifierChanceSource.Succeeds(_critChanceCalculator.GetCritChance(attack.Active, attack.Other, attack.Weapon));
        return isCrit
           ? _modifierChanceSource.ChooseWeighted(_criticalOptions)
           : _modifierChanceSource.ChooseWeighted(_regularOptions);
    }
}