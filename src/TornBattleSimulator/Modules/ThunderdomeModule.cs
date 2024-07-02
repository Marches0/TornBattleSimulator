using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Battle.Thunderdome;

namespace TornBattleSimulator.Modules;

public class ThunderdomeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Thunderdome>()
            .As<Thunderdome>();
    }
}