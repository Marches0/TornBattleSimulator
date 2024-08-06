using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Options;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class BodyPartModifier : IDamageModifier
{
    private readonly RootConfig _rootConfig;
    private readonly IChanceSource _modifierChanceSource;

    private readonly List<OptionChance<BodyPartDamage>> _criticalOptions;
    private readonly List<OptionChance<BodyPartDamage>> _regularOptions;

    public BodyPartModifier(
        BodyModifierOptions bodyModifierOptions,
        RootConfig rootConfig,
        IChanceSource modifierChanceSource)
    {
        _criticalOptions = bodyModifierOptions.CriticalHits
            .Select(h => new OptionChance<BodyPartDamage>(h, h.Chance))
            .ToList();

        _regularOptions = bodyModifierOptions.RegularHits
            .Select(h => new OptionChance<BodyPartDamage>(h, h.Chance))
            .ToList();

        _rootConfig = rootConfig;
        _modifierChanceSource = modifierChanceSource;
    }

    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext)
    {
        BodyPartDamage option = GetTargetBodyPart(weapon);
        damageContext.TargetBodyPart = option.Part;
        return option.DamageMultiplier;
    }

    private BodyPartDamage GetTargetBodyPart(WeaponContext weapon)
    {
        //return _criticalOptions.First(r => r.Option.Part == BodyPart.Head).Option;
        //return _regularOptions.First(r => r.Option.Part == BodyPart.Chest).Option;

        if (weapon.Type == WeaponType.Temporary)
        {
            // Temps hit chest. Thanks Staphy!
            return _regularOptions.First(r => r.Option.Part == BodyPart.Chest).Option;
        }

        // todo: remove this, or make a diff class rather than getting up in this modifier
        if (_rootConfig.BodyPartHitOverride.HasValue)
        {
            return _regularOptions.Concat(_criticalOptions)
                .First(o => o.Option.Part == _rootConfig.BodyPartHitOverride.Value)
                .Option;
        }

        // temp.
        bool isCrit = _modifierChanceSource.Succeeds(0.2d);

        return isCrit
           ? _modifierChanceSource.ChooseWeighted(_criticalOptions)
           : _modifierChanceSource.ChooseWeighted(_regularOptions);
    }
}