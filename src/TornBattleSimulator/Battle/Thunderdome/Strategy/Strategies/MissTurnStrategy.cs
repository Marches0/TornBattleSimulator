using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class MissTurnStrategy : IStrategy
{
    private readonly IChanceSource _chanceSource;

    public MissTurnStrategy(IChanceSource chanceSource)
    {
        _chanceSource = chanceSource;
    }

    public BattleAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other)
    {
        return IsShocked(self) || IsParalysed(self)
            ? BattleAction.MissedTurn
            : null;
    }

    private bool IsShocked(PlayerContext self) => self.Modifiers.Active.Any(m => m is ShockModifier);

    private bool IsParalysed(PlayerContext self) => self.Modifiers.Active.Any(m => m is ParalyzedModifier)
        && _chanceSource.Succeeds(0.5);
}