using System.Text.Json;
using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Modules;

namespace TornBattleSimulator.Battle.Config;

public static class SimulationBuilder
{
    public static SimulatorConfig Build(string configDirectory)
    {
        // todo: validate
        SimulatorConfig file = JsonSerializer.Deserialize<SimulatorConfig>(File.ReadAllText(configDirectory),
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })!;
        return Prepare(file);
    }

    public static SimulatorConfig Prepare(SimulatorConfig raw)
    {
        // Make builds take the first
        // build as the base.
        raw.Builds = raw.Builds
            .Select(b => (BattleBuild)ModelWriter.Apply(raw.Builds[0], b))
            .ToList();

        return raw;
    }
}