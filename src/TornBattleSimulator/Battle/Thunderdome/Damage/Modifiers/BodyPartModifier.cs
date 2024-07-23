using TornBattleSimulator.Options;
using TornBattleSimulator.Shared.Build.Equipment;
using TornBattleSimulator.Shared.Thunderdome.Chance;
using TornBattleSimulator.Shared.Thunderdome.Damage;
using TornBattleSimulator.Shared.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class BodyPartModifier : IDamageModifier
{
    private readonly IChanceSource _modifierChanceSource;
    private bool _isCrit = false; // todo: implement

    private readonly List<OptionChance<BodyPartDamage>> _criticalOptions;
    private readonly List<OptionChance<BodyPartDamage>> _regularOptions;

    public BodyPartModifier(
        BodyModifierOptions bodyModifierOptions,
        IChanceSource modifierChanceSource)
    {
        _criticalOptions = bodyModifierOptions.CriticalHits
            .Select(h => new OptionChance<BodyPartDamage>(h, h.Chance))
            .ToList();

        _regularOptions = bodyModifierOptions.RegularHits
            .Select(h => new OptionChance<BodyPartDamage>(h, h.Chance))
            .ToList();

        _modifierChanceSource = modifierChanceSource;
    }

    public DamageModifierResult GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext)
    {
        BodyPartDamage option = GetTargetBodyPart(weapon);
        damageContext.TargetBodyPart = option.Part;

        return new DamageModifierResult(option.DamageMultiplier, option.Part);
    }

    private BodyPartDamage GetTargetBodyPart(WeaponContext weapon)
    {
        if (weapon.Type == WeaponType.Temporary)
        {
            // Temps hit chest. Thanks Staphy!
            return _regularOptions.First(r => r.Option.Part == BodyPart.Chest).Option;
        }

        return _isCrit
           ? _modifierChanceSource.ChooseWeighted(_criticalOptions)
           : _modifierChanceSource.ChooseWeighted(_regularOptions);
    }
}