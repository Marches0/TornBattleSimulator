﻿using Autofac.Features.Indexed;
using TornBattleSimulator.Battle.Thunderdome.Output;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Events.Data;

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
        _context.Events.Add(_context.CreateEvent(null, ThunderdomeEventType.FightBegin, new FightBeginEvent()));

        while (_context.GetResult() == null)
        {
            MakeMove(_context.Attacker, _context.Defender);
            _context.AttackerActionComplete();

            // todo: only stalemate after defender goes
            if (_context.GetResult() != null)
            {
                break;
            }

            MakeMove(_context.Defender, _context.Attacker);

            _context.DefenderActionComplete();
            _context.TurnComplete();
        }

        _context.Events.Add(_context.CreateEvent(null, ThunderdomeEventType.FightEnd, new FightEndEvent(_context.GetResult()!.Value)));
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