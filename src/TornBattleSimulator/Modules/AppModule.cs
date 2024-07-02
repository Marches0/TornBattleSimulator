using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TornBattleSimulator.Modules;

internal class AppModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Runner>()
            .As<Runner>()
            .SingleInstance();

        builder.RegisterModule<MapperModule>();
    }
}