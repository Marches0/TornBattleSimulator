using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Strategy;

namespace TornBattleSimulator.Modules;

public class StrategyModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StrategyBuilder>()
            .As<StrategyBuilder>();
    }
}