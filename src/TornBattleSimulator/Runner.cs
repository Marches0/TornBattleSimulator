using AutoMapper;
using TornBattleSimulator.Battle.Config;
using TornBattleSimulator.Input;

namespace TornBattleSimulator;

public class Runner
{
    private readonly IMapper _mapper;

    public Runner(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Start(string configFile)
    {
        SimulatorInput simulation = SimulationBuilder.Build(configFile);
        SimulatorConfig actual = _mapper.Map<SimulatorInput, SimulatorConfig>(simulation);
    }
}