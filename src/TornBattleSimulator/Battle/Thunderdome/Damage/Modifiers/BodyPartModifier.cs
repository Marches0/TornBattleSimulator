using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Chance;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Options;

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
        if (weapon.Type == WeaponType.Temporary)
        {
            // Thanks Staphy!
            BodyPartDamage chest = _regularOptions.First(r => r.Option.Part == BodyPart.Chest).Option;
            return new DamageModifierResult(chest.DamageMultiplier, chest.Part);
        }

        BodyPartDamage option = _isCrit
            ? _modifierChanceSource.ChooseWeighted(_criticalOptions)
            : _modifierChanceSource.ChooseWeighted(_regularOptions);

        damageContext.TargetBodyPart = option.Part;

        return new DamageModifierResult(option.DamageMultiplier, option.Part);
    }
}