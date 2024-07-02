using TornBattleSimulator.Battle.Config;

namespace TornBattleSimulator;

public class Runner
{

    public Runner()
    {
    }

    public async Task Start(string configFile)
    {
        var simulation = SimulationBuilder.Build(configFile);
    }
}