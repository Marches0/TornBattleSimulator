﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TornBattleSimulator.Battle.Thunderdome.Damage;

public interface IDamageCalculator
{
    int CalculateDamage(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other);
}