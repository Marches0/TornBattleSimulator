using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Strategy;
using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

namespace TornBattleSimulator.Modules;

public class StrategyModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StrategyBuilder>()
            .As<StrategyBuilder>();

        builder.RegisterType<StunStrategy>()
            .As<StunStrategy>();
    }
}