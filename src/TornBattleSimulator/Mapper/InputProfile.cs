using AutoMapper;
using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Config;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Description;
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
        CreateMap<ModifierInput, WeaponModifierDescription>();

        CreateMap<ArmourSetInput, ArmourSet>();
        CreateMap<ArmourInput, Armour>();

        CreateMap<StrategyInput, StrategyDescription>();
        CreateMap<StrategyUntilInput, StrategyUntil>();

        // not actually needed? hmm
        /*CreateMap<string, WeaponType>()
            .ConvertUsing(new StringEnumTypeConverter<WeaponType>());*/
    }

    private class StringEnumTypeConverter<TEnum> : ITypeConverter<string, TEnum> where TEnum : struct, Enum
    {
        public TEnum Convert(string source, TEnum destination, ResolutionContext context)
        {
            return EnumBuilder.GetAsEnum<TEnum>(source);
        }
    }
}