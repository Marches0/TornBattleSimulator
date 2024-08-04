using Autofac;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Output;
using TornBattleSimulator.Battle.Thunderdome.Target;

namespace TornBattleSimulator.Modules;

public class ThunderdomeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Thunderdome>()
            .As<Thunderdome>();

        builder.RegisterType<ThunderdomeResultWriter>();

        builder.RegisterType<TargetSelector>()
            .As<TargetSelector>();
    }
}