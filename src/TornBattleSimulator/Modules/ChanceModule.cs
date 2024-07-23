using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Chance.Source;
using TornBattleSimulator.Battle.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Chance.Source;
using TornBattleSimulator.Core.Thunderdome.Chance;

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