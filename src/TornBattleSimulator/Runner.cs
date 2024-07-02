using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Config;
using TornBattleSimulator.Modules;

namespace TornBattleSimulator;

public class Runner
{

    public Runner()
    {
    }

    public async Task Start(string configFile)
    {
        SimulatorConfig simulation = SimulationBuilder.Build(configFile);
    }
}