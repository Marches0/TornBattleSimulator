using Autofac.Features.Indexed;
using TornBattleSimulator.Battle.Thunderdome.Action;

namespace TornBattleSimulator.Battle.Thunderdome;

public class Thunderdome
{
    private readonly ThunderdomeContext _context;
    private readonly IIndex<BattleAction, IAction> _actions;

    public delegate Thunderdome Create(ThunderdomeContext context);
    
    public Thunderdome(
        ThunderdomeContext context,
        IIndex<BattleAction, IAction> actions)
    {
        _context = context;
        _actions = actions;
    }

    public void Battle()
    {
        while (_context.GetResult() == null)
        {
            _context.Tick();
            MakeMove(_context.Attacker, _context.Defender);

            if (_context.GetResult() != null)
            {
                break;
            }

            MakeMove(_context.Defender, _context.Attacker);
        }

        Console.WriteLine(_context.GetResult());
    }

    private void MakeMove(PlayerContext active, PlayerContext other)
    {
        BattleAction move = active.Strategy.GetMove(_context, active, other)!.Value;
        active.CurrentAction = move;

        IAction action = _actions[active.Strategy.GetMove(_context, active, other)!.Value];
        ThunderdomeEvent result = action.PerformAction(_context, active, other);

        _context.Events.Add(result);
    }
}