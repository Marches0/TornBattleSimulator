using Autofac;

namespace TornBattleSimulator.Modules;

internal class AppModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Runner>()
            .As<Runner>()
            .SingleInstance();

        builder
            .RegisterModule<MapperModule>()
            .RegisterModule<DamageModule>()
            .RegisterModule<ThunderdomeModule>()
            .RegisterModule<ActionModule>()
            .RegisterModule<StrategyModule>()
            .RegisterModule<GearModule>();
    }
}