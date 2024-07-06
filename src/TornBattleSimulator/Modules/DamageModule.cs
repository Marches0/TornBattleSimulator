using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Modules;
public class DamageModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DamageCalculator>()
            .As<IDamageCalculator>()
            .SingleInstance();

        builder.RegisterType<StrengthDefenceRatioDamageModifier>()
            .As<IDamageModifier>();

        builder.RegisterType<StrengthDamageModifier>()
            .As<IDamageModifier>();

        builder.RegisterType<WeaponDamageModifier>()
            .As<IDamageModifier>();

        builder.RegisterType<BodyPartModifier>()
            .As<IDamageModifier>();

        builder.RegisterType<ArmourDamageModifier>()
            .As<IDamageModifier>();
    }
}