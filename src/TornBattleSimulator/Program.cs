using Autofac;
using TornBattleSimulator;
using TornBattleSimulator.Modules;

ContainerBuilder container = new();
container.RegisterModule<AppModule>();
await container.Build().Resolve<Runner>().Start(@"C:\Users\Marches\source\repos\Marches0\TornBattleSimulator\src\test.json");