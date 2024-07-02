using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Strategy;

namespace TornBattleSimulator.Modules;

public class ActionModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AttackPrimaryAction>()
            .Keyed<IAction>(BattleAction.AttackPrimary);
    }
}