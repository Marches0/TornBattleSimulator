using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Thunderdome;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class PlayerContextBuilder
{
    private BattleStats? _battleStats;

    public PlayerContextBuilder WithStats(BattleStats battleStats)
    {
        _battleStats = battleStats;
        return this;
    }

    public PlayerContext Build()
    {
        return new PlayerContext(
            new BattleBuild()
            {
                BattleStats = _battleStats
            }
        );
    }
}