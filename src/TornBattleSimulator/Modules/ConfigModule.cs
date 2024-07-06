using Autofac;
using Microsoft.Extensions.Configuration;
using TornBattleSimulator.Options;

namespace TornBattleSimulator.Modules;

public class ConfigModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(ctx => new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
            )
            .As<IConfiguration>()
            .SingleInstance();

        builder.Register(ctx => ctx.Resolve<IConfiguration>().Get<RootConfig>()!)
            .As<RootConfig>()
            .SingleInstance();

        builder.Register(ctx => ctx.Resolve<RootConfig>().BodyModifier)
            .As<BodyModifierOptions>()
            .SingleInstance();

        builder.Register(ctx => ctx.Resolve<RootConfig>().ArmourCoverage)
            .As<List<ArmourCoverageOption>>()
            .SingleInstance();
    }
}