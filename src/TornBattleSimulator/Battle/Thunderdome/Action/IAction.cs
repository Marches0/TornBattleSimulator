using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TornBattleSimulator.Battle.Thunderdome.Action;

public interface IAction
{
    void PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other);
}