using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Player;

namespace TornBattleSimulator.Modules;

public class GearModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<WeaponsFactory>()
            .As<WeaponsFactory>();
    }
}