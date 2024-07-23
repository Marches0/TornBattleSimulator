using AutoMapper;
using TornBattleSimulator.Battle.Config;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Player.Armours;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Battle.Thunderdome.Strategy;
using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Input;

namespace TornBattleSimulator;

public class Runner
{
    private readonly IMapper _mapper;
    private readonly Thunderdome.Create _thunderdomeFactory;
    private readonly WeaponsFactory _weaponsFactory;
    private readonly ArmourFactory _armourFactory;
    private readonly StrategyBuilder _strategyBuilder;

    public Runner(
        IMapper mapper,
        Thunderdome.Create thunderdomeFactory,
        WeaponsFactory weaponsFactory,
        ArmourFactory armourFactory,
        StrategyBuilder strategyBuilder)
    {
        _mapper = mapper;
        _thunderdomeFactory = thunderdomeFactory;
        _weaponsFactory = weaponsFactory;
        _armourFactory = armourFactory;
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
                //yield return CreateThunderdome(config.Builds[j], config.Builds[i]);
            }
        }
    }

    private Thunderdome CreateThunderdome(
        BattleBuild attacker,
        BattleBuild defender)
    {
        return _thunderdomeFactory(
            new ThunderdomeContext(
                new PlayerContext(attacker, PlayerType.Attacker, _weaponsFactory.Create(attacker), _armourFactory.Create(attacker.Armour), _strategyBuilder.BuildStrategy(attacker)),
                new PlayerContext(defender, PlayerType.Defender, _weaponsFactory.Create(defender), _armourFactory.Create(defender.Armour), _strategyBuilder.BuildStrategy(defender))
            )
        );
    }
}