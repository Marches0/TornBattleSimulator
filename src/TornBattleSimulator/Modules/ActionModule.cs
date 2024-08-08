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
        builder.RegisterType<AttackAction>()
            .Keyed<IAction>(BattleAction.Attack);

        builder.RegisterType<ReloadAction>()
            .Keyed<IAction>(BattleAction.Reload);

        builder.RegisterType<ChargeAction>()
            .Keyed<IAction>(BattleAction.Charge);

        builder.RegisterType<DisarmedAction>()
            .Keyed<IAction>(BattleAction.Disarmed);

        builder.RegisterType<MissTurnAction>()
            .Keyed<IAction>(BattleAction.MissedTurn);

        builder.RegisterType<ReplenishTemporaryAction>()
            .Keyed<IAction>(BattleAction.ReplenishTemporary);

        builder.RegisterType<WeaponUsage>()
            .As<IWeaponUsage>();

        builder.RegisterType<AmmoCalculator>()
            .As<IAmmoCalculator>();

        builder.RegisterType<DamageProcessor>()
            .As<DamageProcessor>();
    }
}