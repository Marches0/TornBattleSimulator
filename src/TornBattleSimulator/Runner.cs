using AutoMapper;
using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Config;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Strategy;
using TornBattleSimulator.Input;

namespace TornBattleSimulator;

public class Runner
{
    private readonly IMapper _mapper;
    private readonly Thunderdome.Create _thunderdomeFactory;
    private readonly StrategyBuilder _strategyBuilder;

    public Runner(
        IMapper mapper,
        Thunderdome.Create thunderdomeFactory,
        StrategyBuilder strategyBuilder)
    {
        _mapper = mapper;
        _thunderdomeFactory = thunderdomeFactory;
        _strategyBuilder = strategyBuilder;
    }

    public async Task Start(string configFile)
    {
        SimulatorInput simulation = SimulationBuilder.Build(configFile);
        SimulatorConfig actual = _mapper.Map<SimulatorInput, SimulatorConfig>(simulation);

        foreach (Thunderdome battle in GetBattles(actual))
        {
            battle.Battle();
        }
    }

    private IEnumerable<Thunderdome> GetBattles(SimulatorConfig config)
    {
        for (int i = 0; i < config.Builds.Count; i++)
        {
            for (int j = i + 1;  j < config.Builds.Count; j++)
            {
                yield return CreateThunderdome(config.Builds[i], config.Builds[j]);
                yield return CreateThunderdome(config.Builds[j], config.Builds[i]);
            }
        }
    }

    private Thunderdome CreateThunderdome(
        BattleBuild attacker,
        BattleBuild defender)
    {
        return _thunderdomeFactory(
            new ThunderdomeContext(
                new PlayerContext(attacker, _strategyBuilder.BuildStrategy(attacker)),
                new PlayerContext(defender, _strategyBuilder.BuildStrategy(defender))
            )
        );
    }
}