using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TornBattleSimulator.Mapper;

namespace TornBattleSimulator.UnitTests.Mapper;

[TestFixture]
public class MapperTests
{
    [Test]
    public void MapperConfig_IsValid()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<InputProfile>();
        });

        config.AssertConfigurationIsValid();
    }
}