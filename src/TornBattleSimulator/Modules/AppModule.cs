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
            .RegisterModule<AccuracyModule>()
            .RegisterModule<ActionModule>()
            .RegisterModule<ChanceModule>()
            .RegisterModule<ConfigModule>()
            .RegisterModule<DamageModule>()
            .RegisterModule<GearModule>()
            .RegisterModule<MapperModule>()
            .RegisterModule<ModifierModule>()
            .RegisterModule<ThunderdomeModule>()
            .RegisterModule<StrategyModule>();
    }
}