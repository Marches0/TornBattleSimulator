using AutoMapper;
using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Config;
using TornBattleSimulator.Battle.Thunderdome.Strategy;
using TornBattleSimulator.Input;
using TornBattleSimulator.Input.Build;
using TornBattleSimulator.Input.Build.Gear;
using TornBattleSimulator.Input.Build.Stats;

namespace TornBattleSimulator.Mapper;

public class InputProfile : Profile
{
    public InputProfile()
    {
        CreateMap<SimulatorInput, SimulatorConfig>();
        CreateMap<BuildInput, BattleBuild>();

        CreateMap<BattleStatsInput, BattleStats>();

        CreateMap<WeaponInput, Weapon>();
        CreateMap<AmmoInput, Ammo>();
        CreateMap<RateOfFireInput, RateOfFire>();

        CreateMap<StrategyInput, StrategyDescription>();
        CreateMap<StrategyUntilInput, StrategyUntil>();

        // todo
        /*CreateMap<string, WeaponType>()
            .ConstructUsing(_ => 0);*/
    }
}