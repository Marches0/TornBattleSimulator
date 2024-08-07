using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Critical;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Options;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers.BodyParts;

public class BodyPartModifier : IDamageModifier
{
    private readonly ICritChanceCalculator _critChanceCalculator;
    private readonly IChanceSource _modifierChanceSource;

    private readonly List<OptionChance<BodyPartDamage>> _criticalOptions;
    private readonly List<OptionChance<BodyPartDamage>> _regularOptions;

    public BodyPartModifier(
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

    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext)
    {
        BodyPartDamage option = GetTargetBodyPart(active, other, weapon);
        damageContext.TargetBodyPart = option.Part;
        return option.DamageMultiplier;
    }

    private BodyPartDamage GetTargetBodyPart(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        if (weapon.Type == WeaponType.Temporary)
        {
            // Temps hit chest. Thanks Staphy!
            return _regularOptions.First(r => r.Option.Part == BodyPart.Chest).Option;
        }

        bool isCrit = _modifierChanceSource.Succeeds(_critChanceCalculator.GetCritChance(active, other, weapon));
        return isCrit
           ? _modifierChanceSource.ChooseWeighted(_criticalOptions)
           : _modifierChanceSource.ChooseWeighted(_regularOptions);
    }
}