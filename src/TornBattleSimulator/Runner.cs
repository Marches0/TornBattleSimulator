using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TornBattleSimulator.Modules;

namespace TornBattleSimulator;

public class Runner
{

    public Runner()
    {
    }

    public async Task Start(string fightDefinitonFile)
    {
        var file = JsonSerializer.Deserialize<FileType>(File.ReadAllText(fightDefinitonFile), new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        var xx = ModelWriter.Apply(file.Things[0], file.Things[1]);
    }
}

public class FileType
{
    public List<TestThing>? Things { get; set; }
}

public class TestThing
{
    public string? Name { get; set; }
    public Nested? Nested { get; set; }
    public List<int>? Numbers { get; set; }
}

public class Nested
{
    public string? Name { get; set; }
}