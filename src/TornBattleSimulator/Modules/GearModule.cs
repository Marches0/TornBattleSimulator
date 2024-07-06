using Autofac;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Modifiers.Temporary;
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

        // modifiers somewhere else?
        builder.RegisterType<ModifierFactory>()
            .As<ModifierFactory>();

        builder.RegisterType<ModifierApplier>()
            .As<ModifierApplier>();

        RegisterTemporaryWeapons(builder);
    }

    private void RegisterTemporaryWeapons(ContainerBuilder builder)
    {
        builder.RegisterType<TemporaryWeaponFactory>()
            .As<TemporaryWeaponFactory>();

        builder.RegisterType<TearGasModifier>()
            .Keyed<IModifier>(ModifierType.Gassed);
    }
}