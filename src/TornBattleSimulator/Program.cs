using Autofac;
using TornBattleSimulator;
using TornBattleSimulator.Modules;

var container = new ContainerBuilder();
container.RegisterModule<AppModule>();
await container.Build().Resolve<Runner>().Start(@"C:\Users\Marches\Downloads\test.json");