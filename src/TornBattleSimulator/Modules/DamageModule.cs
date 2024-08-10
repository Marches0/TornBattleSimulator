using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers.BodyParts;
using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;

namespace TornBattleSimulator.Modules;
public class DamageModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DamageCalculator>()
            .As<IDamageCalculator>()
            .SingleInstance();

        builder.RegisterType<DamageTargeter>()
            .As<IDamageTargeter>();

        builder.RegisterType<HitLocationCalculator>()
            .As<IHitLocationCalculator>();

        builder.RegisterType<HitArmourCalculator>()
            .As<IHitArmourCalculator>();

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