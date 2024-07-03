using Autofac;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Modifiers.Temporary;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Modules;

public class GearModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<WeaponsFactory>()
            .As<WeaponsFactory>();

        builder.RegisterType<ModifierFactory>()
            .As<ModifierFactory>();

        RegisterTemporaryWeapons(builder);
    }

    private void RegisterTemporaryWeapons(ContainerBuilder builder)
    {
        builder.RegisterType<TemporaryWeaponFactory>()
            .As<TemporaryWeaponFactory>();

        builder.RegisterType<TearGasModifier>()
            .Keyed<IModifier>(WeaponModifierType.Gassed);
    }
}