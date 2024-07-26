using Autofac.Features.Indexed;
using TornBattleSimulator.Battle.Thunderdome.Output;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Modifiers;

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

        ActivateModifers(_context, _context.Attacker);
        ActivateModifers(_context, _context.Defender);

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

    private void ActivateModifers(ThunderdomeContext context, PlayerContext player)
    {
        if (player.Weapons.Primary != null)
        {
            ActivateModifers(context, player, player.Weapons.Primary);
        }

        if (player.Weapons.Secondary != null)
        {
            ActivateModifers(context, player, player.Weapons.Secondary);
        }

        if (player.Weapons.Melee != null)
        {
            ActivateModifers(context, player, player.Weapons.Melee);
        }

        if (player.Weapons.Temporary != null)
        {
            ActivateModifers(context, player, player.Weapons.Temporary);
        }
    }

    private void ActivateModifers(ThunderdomeContext context, PlayerContext player, WeaponContext weapon)
    {
        // Not good. but here we are.
        weapon.ActiveModifiers = new ModifierContext(player);

        foreach (var modifier in weapon.Modifiers
            .Select(m => m.Modifier)
            .Where(m => m.AppliesAt == ModifierApplication.FightStart)
            .Where(m => m.Target == ModifierTarget.Self))
        {
            if (weapon.ActiveModifiers.AddModifier(modifier, null))
            {
                context.Events.Add(
                    context.CreateEvent(player, ThunderdomeEventType.EffectBegin, new EffectBeginEvent(modifier.Effect))
                );
            }
        }
    }
}