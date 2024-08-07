using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Attacks;
using TornBattleSimulator.Core.Thunderdome.Damage.Critical;

namespace TornBattleSimulator.Modules;

public class ModifierModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ModifierFactory>()
            .As<ModifierFactory>();

        builder.RegisterType<ModifierApplier>()
            .As<IModifierApplier>();

        builder.RegisterType<ModifierRoller>()
            .As<ModifierRoller>();

        builder.RegisterType<HealthModifierApplier>()
            .As<IHealthModifierApplier>();

        builder.RegisterType<AttackModifierApplier>()
            .As<AttackModifierApplier>();

        builder.RegisterType<CritChanceCalculator>()
            .As<ICritChanceCalculator>();
    }
}