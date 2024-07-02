using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Mapper;

namespace TornBattleSimulator.Modules;

public class MapperModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(_ => new MapperConfiguration(opts =>
            {
                opts.AddProfile<InputProfile>();
            }).CreateMapper()
        ).As<IMapper>()
        .SingleInstance();
    }
}