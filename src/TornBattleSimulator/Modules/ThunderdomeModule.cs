using Autofac;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Output;

namespace TornBattleSimulator.Modules;

public class ThunderdomeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Thunderdome>()
            .As<Thunderdome>();

        builder.RegisterType<ThunderdomeResultWriter>();
    }
}