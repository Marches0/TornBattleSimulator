using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

public interface IModifier
{
    float TimeRemainingSeconds { get; set; }
}