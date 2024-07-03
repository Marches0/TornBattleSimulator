using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

namespace TornBattleSimulator.Modules;

public class ActionModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AttackPrimaryAction>()
            .Keyed<IAction>(BattleAction.AttackPrimary);

        builder.RegisterType<AttackSecondaryAction>()
            .Keyed<IAction>(BattleAction.AttackSecondary);

        builder.RegisterType<ReloadPrimaryAction>()
            .Keyed<IAction>(BattleAction.ReloadPrimary);

        builder.RegisterType<ReloadSecondaryAction>()
            .Keyed<IAction>(BattleAction.ReloadSecondary);
    }
}