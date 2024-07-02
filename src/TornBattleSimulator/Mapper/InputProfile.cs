using AutoMapper;
using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Config;
using TornBattleSimulator.Input;
using TornBattleSimulator.Input.Build;
using TornBattleSimulator.Input.Build.Stats;

namespace TornBattleSimulator.Mapper;

public class InputProfile : Profile
{
    public InputProfile()
    {
        CreateMap<SimulatorInput, SimulatorConfig>();
        CreateMap<BuildInput, BattleBuild>();
        CreateMap<BattleStatsInput, BattleStats>();
    }
}