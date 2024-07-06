using TornBattleSimulator.Battle.Thunderdome.Chance.Source;

namespace TornBattleSimulator.Battle.Thunderdome.Chance;

public class RandomModifierChanceSource : IModifierChanceSource
{
    private readonly IRandomSource _source;

    public RandomModifierChanceSource(
        IRandomSource source)
    {
        _source = source;
    }

    public bool Succeeds(double probability) => _source.Next() <= probability;

    public T ChooseWeighted<T>(IList<OptionChance<T>> options)
    {
        // Find the first block of probability that captures something
        // for a [0.1, 0.1, 0.1, 0.7] block
        // 0.25 fits in the third, since the first two 0.1 take up the 0.2
        // and so the remaining 0.05 falls in the third 0.1.
        double roll = _source.Next();
        double aggregate = 0;

        foreach (OptionChance<T> option in options)
        {
            aggregate += option.Chance;

            if (aggregate < roll)
            {
                return option.Option;
            }
        }

        // Weird
        return options.Last().Option;
    }
}