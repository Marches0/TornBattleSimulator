using Autofac;
using TornBattleSimulator.Battle.Thunderdome;

namespace TornBattleSimulator.Modules;

public class ThunderdomeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Thunderdome>()
            .As<Thunderdome>();
    }
}