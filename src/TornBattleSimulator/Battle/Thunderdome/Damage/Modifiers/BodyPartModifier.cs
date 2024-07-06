using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application.Chance;
using TornBattleSimulator.Options;

namespace TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

public class BodyPartModifier : IDamageModifier
{
    private readonly IModifierChanceSource _modifierChanceSource;
    private bool _isCrit = false; // todo: implement

    private readonly List<OptionChance<BodyPartDamage>> _criticalOptions;
    private readonly List<OptionChance<BodyPartDamage>> _regularOptions;

    public BodyPartModifier(
        BodyModifierOptions bodyModifierOptions,
        IModifierChanceSource modifierChanceSource)
    {
        _criticalOptions = bodyModifierOptions.CriticalHits
            .Select(h => new OptionChance<BodyPartDamage>(h, h.Chance))
            .ToList();

        _regularOptions = bodyModifierOptions.RegularHits
            .Select(h => new OptionChance<BodyPartDamage>(h, h.Chance))
            .ToList();

        _modifierChanceSource = modifierChanceSource;
    }

    public double GetDamageModifier(PlayerContext active, PlayerContext other)
    {
        BodyPartDamage option = _isCrit
            ? _modifierChanceSource.ChooseWeighted(_criticalOptions)
            : _modifierChanceSource.ChooseWeighted(_regularOptions);

        return option.DamageMultiplier;
    }
}