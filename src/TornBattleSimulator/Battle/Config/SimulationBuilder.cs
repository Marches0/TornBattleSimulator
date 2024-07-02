using System.Text.Json;
using TornBattleSimulator.Input;
using TornBattleSimulator.Input.Build;
using TornBattleSimulator.Modules;

namespace TornBattleSimulator.Battle.Config;

public static class SimulationBuilder
{
    public static SimulatorInput Build(string configDirectory)
    {
        // todo: validate
        SimulatorInput file = JsonSerializer.Deserialize<SimulatorInput>(File.ReadAllText(configDirectory),
            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })!;
        return Prepare(file);
    }

    public static SimulatorInput Prepare(SimulatorInput raw)
    {
        // Make builds take the first
        // build as the base.
        raw.Builds = raw.Builds!
            .Select(b => (BuildInput)ModelWriter.Apply(raw.Builds![0], b))
            .ToList();

        return raw;
    }
}