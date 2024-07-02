using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Modules;
public class DamageModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DamageCalculator>()
            .As<DamageCalculator>()
            .SingleInstance();

        builder.RegisterType<StrengthDefenceRatioDamageModifier>()
            .As<IDamageModifier>();

        builder.RegisterType<StrengthDamageModifier>()
            .As<IDamageModifier>();
    }
}