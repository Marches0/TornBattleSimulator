using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Accuracy;
using TornBattleSimulator.Battle.Thunderdome.Accuracy.Modifiers;

namespace TornBattleSimulator.Modules;

public class AccuracyModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AccuracyCalculator>()
            .As<IAccuracyCalculator>();

        builder.RegisterType<SpeedDexterityAccuracyModifier>()
            .As<ISpeedDexterityAccuracyModifier>();

        builder.RegisterType<WeaponAccuracyModifier>()
            .As<IWeaponAccuracyModifier>();
    }
}