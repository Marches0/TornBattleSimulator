using Autofac.Features.Indexed;
using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Output;
using TornBattleSimulator.Battle.Thunderdome.Events;

namespace TornBattleSimulator.Battle.Thunderdome;

public class Thunderdome
{
    private readonly ThunderdomeContext _context;
    private readonly ThunderdomeResultWriter _resultWriter;
    private readonly IIndex<BattleAction, IAction> _actions;

    public delegate Thunderdome Create(ThunderdomeContext context);
    
    public Thunderdome(
        ThunderdomeContext context,
        ThunderdomeResultWriter resultWriter,
        IIndex<BattleAction, IAction> actions)
    {
        _context = context;
        _resultWriter = resultWriter;
        _actions = actions;
    }

    public void Battle()
    {
        while (_context.GetResult() == null)
        {
            _context.Tick();
            MakeMove(_context.Attacker, _context.Defender);
            _context.TurnComplete(); // hmm

            if (_context.GetResult() != null)
            {
                break;
            }

            MakeMove(_context.Defender, _context.Attacker);
            _context.TurnComplete();
        }

        _resultWriter.Write(_context);
    }

    private void MakeMove(PlayerContext active, PlayerContext other)
    {
        BattleAction move = active.Strategy.GetMove(_context, active, other)!.Value;
        active.CurrentAction = move;

        IAction action = _actions[active.Strategy.GetMove(_context, active, other)!.Value];
        List<ThunderdomeEvent> result = action.PerformAction(_context, active, other);

        _context.Events.AddRange(result);
    }
}