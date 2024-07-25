using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Attacks;

namespace TornBattleSimulator.Modules;

public class ModifierModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ModifierFactory>()
            .As<ModifierFactory>();

        builder.RegisterType<ModifierApplier>()
            .As<ModifierApplier>();

        builder.RegisterType<AttackModifierApplier>()
            .As<AttackModifierApplier>();
    }
}