using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Chance.Source;
using TornBattleSimulator.Battle.Thunderdome.Chance;
using TornBattleSimulator.Shared.Thunderdome.Chance.Source;
using TornBattleSimulator.Shared.Thunderdome.Chance;

namespace TornBattleSimulator.Modules;

public class ChanceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RandomChanceSource>()
            .As<IChanceSource>();

        builder.RegisterType<RandomSource>()
            .As<IRandomSource>();
    }
}