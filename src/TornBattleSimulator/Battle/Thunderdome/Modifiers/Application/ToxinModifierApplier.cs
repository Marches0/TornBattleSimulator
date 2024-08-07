using TornBattleSimulator.BonusModifiers.Stats.Weapon;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;

public class ToxinModifierApplier : IToxinModifierApplier
{
    private readonly IChanceSource _chanceSource;

    private List<OptionChance<IModifier>> _possibleModifiers = [
        new OptionChance<IModifier>(new CrippleModifier(), 0.25),
        new OptionChance<IModifier>(new SlowModifier(), 0.25),
        new OptionChance<IModifier>(new WeakenModifier(), 0.25),
        new OptionChance<IModifier>(new WitherModifier(), 0.25),
    ];

    public ToxinModifierApplier(IChanceSource chanceSource)
    {
        _chanceSource = chanceSource;
    }

    public IModifier GetModifier()
    {
        return _chanceSource.ChooseWeighted(_possibleModifiers);
    }
}