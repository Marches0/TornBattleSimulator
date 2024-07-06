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

        builder.Register(ctx => ctx.Resolve<IConfiguration>().GetRequiredSection("BodyModifier").Get<BodyModifierOptions>()!)
            .As<BodyModifierOptions>()
            .SingleInstance();
    }
}