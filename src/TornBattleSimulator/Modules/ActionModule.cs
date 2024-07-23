using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;
using TornBattleSimulator.Core.Thunderdome.Actions;

namespace TornBattleSimulator.Modules;

public class ActionModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterAttacks(builder);
        RegisterReloads(builder);
        RegisterCharges(builder);

        builder.RegisterType<UseTemporaryAction>()
            .Keyed<IAction>(BattleAction.UseTemporary);

        builder.RegisterType<StunnedAction>()
            .Keyed<IAction>(BattleAction.Stunned);

        builder.RegisterType<WeaponUsage>()
            .As<IWeaponUsage>();
    }

    private void RegisterAttacks(ContainerBuilder builder)
    {
        builder.RegisterType<AttackPrimaryAction>()
           .Keyed<IAction>(BattleAction.AttackPrimary);

        builder.RegisterType<AttackSecondaryAction>()
            .Keyed<IAction>(BattleAction.AttackSecondary);

        builder.RegisterType<AttackMeleeAction>()
            .Keyed<IAction>(BattleAction.AttackMelee);
    }

    private void RegisterReloads(ContainerBuilder builder)
    {
        builder.RegisterType<ReloadPrimaryAction>()
           .Keyed<IAction>(BattleAction.ReloadPrimary);

        builder.RegisterType<ReloadSecondaryAction>()
            .Keyed<IAction>(BattleAction.ReloadSecondary);
    }

    private void RegisterCharges(ContainerBuilder builder)
    {
        builder.RegisterType<ChargePrimaryAction>()
            .Keyed<IAction>(BattleAction.ChargePrimary);

        builder.RegisterType<ChargeSecondaryAction>()
            .Keyed<IAction>(BattleAction.ChargeSecondary);

        builder.RegisterType<ChargeMeleeAction>()
            .Keyed<IAction>(BattleAction.ChargeMelee);
    }
}