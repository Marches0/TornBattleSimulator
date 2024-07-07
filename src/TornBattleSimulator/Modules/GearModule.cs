using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Player.Armours;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Modules;

public class GearModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<WeaponsFactory>()
            .As<WeaponsFactory>();

        builder.RegisterType<ArmourFactory>()
            .As<ArmourFactory>()
            .SingleInstance();

        builder.RegisterType<TemporaryWeaponFactory>()
            .As<TemporaryWeaponFactory>()
            .SingleInstance();
    }
}