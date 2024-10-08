﻿using Spectre.Console;
using System.Numerics;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Output;

public class ThunderdomeResultWriter
{
    private static Dictionary<ThunderdomeEventType, string> EventColours = new Dictionary<ThunderdomeEventType, string>()
    {
        {ThunderdomeEventType.AttackHit, "#77dd77" }, // green
        {ThunderdomeEventType.AttackMiss, "#cfcfc4" }, // grey
        {ThunderdomeEventType.UsedTemporary, "#c49bdd" }, // grey
        {ThunderdomeEventType.Reload, "#cfcfc4" }, // grey
        {ThunderdomeEventType.MissedTurn, "#ff7974" }, // red
        {ThunderdomeEventType.EffectBegin, "#c49bdd" }, // purple
        {ThunderdomeEventType.EffectEnd, "#cfcfc4" }, // grey
        {ThunderdomeEventType.DamageOverTime, "#c49bdd" }, // purple
        {ThunderdomeEventType.Heal, "#c49bdd" }, // purple
        {ThunderdomeEventType.ExtraDamage, "#c49bdd" }, // purple
        {ThunderdomeEventType.FightBegin, "#ffffff" }, // white
        {ThunderdomeEventType.FightEnd, "#ffffff" }, // white
        {ThunderdomeEventType.ChargeWeapon, "#c49bdd" }, // purple
        {ThunderdomeEventType.Disarmed, "#c49bdd" }, // purple
        {ThunderdomeEventType.ReplenishTemporary, "#c49bdd" }, // purple
    };

    public void Write(ThunderdomeContext context)
    {
        Table table = new Table()
            .NoBorder()
            //.LeftAligned()
            .Centered()
            .Title($"{context.Attacker.Build.Name.EscapeMarkup()} attacking {context.Defender.Build.Name.EscapeMarkup()} ({context.GetResult()})");

        TableColumn attEvtColumn = DefaultColumn("ATT Event".ToColouredString("#C1E1C1"));
        attEvtColumn.RightAligned();
        attEvtColumn.Padding = new Padding(0, 1, 3, 0);

        TableColumn defEvtColumn = DefaultColumn("DEF Event".ToColouredString("#FAA0A0"));
        defEvtColumn.LeftAligned();
        defEvtColumn.Padding = new Padding(0, 1, 1, 0);

        table.AddColumns(
            DefaultColumn("T"),
            DefaultColumn("ATT DEX"),
            DefaultColumn("ATT SPD"),
            DefaultColumn("ATT DEF"),
            DefaultColumn("ATT Str"),
            DefaultColumn("ATT HP"),
            attEvtColumn,
            DefaultColumn("Details"),
            defEvtColumn,
            DefaultColumn("DEF HP"),
            DefaultColumn("DEF Str"),
            DefaultColumn("DEF DEF"),
            DefaultColumn("DEF SPD"),
            DefaultColumn("DEF DEX"),
            DefaultColumn("T")
        );

        //table.AddColumns("T", "Player", "Event", "Details", "ATT HP", "DEF HP", "ATT Str", "ATT DEF", "ATT SPD", "ATT DEX", "DEF Str", "DEF DEF", "DEF SPD", "DEF DEX");
        
        // todo: make sure we capture last event
        table.AddRow(ToRow(context.Events[0], null));

        // Too much effort to LINQ
        ThunderdomeEvent previousEvent = context.Events[0];
        foreach (ThunderdomeEvent currentEvent in context.Events.Skip(1))
        {
            table.AddRow(ToRow(currentEvent, previousEvent));
            previousEvent = currentEvent;
        }

        AnsiConsole.Write(table);
    }

    private string[] ToRow(
        ThunderdomeEvent currentEvent,
        ThunderdomeEvent? previousEvent
        )
    {
        string currentEventType = currentEvent.Type.ToString().ToColouredString(EventColours[currentEvent.Type]);
        string arrowedEvent = currentEvent.Source == PlayerType.Attacker
            ? currentEventType + " -->"
            : "<-- " + currentEventType;

        return [
            currentEvent.Turn.ToString(),
            GetDiffSelector(currentEvent, previousEvent, e => e.AttackerStats.Dexterity, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.AttackerStats.Speed, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.AttackerStats.Defence, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.AttackerStats.Strength, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.AttackerHealth, x => x.ToString("n0")),
            
            currentEvent.Source == PlayerType.Attacker ? arrowedEvent : " ",
            currentEvent.Data.Format().ToColouredString(currentEvent.Source == PlayerType.Attacker ? "#C1E1C1" : "#FAA0A0"),
            currentEvent.Source == PlayerType.Defender ? arrowedEvent : " ",
            GetDiffSelector(currentEvent, previousEvent, e => e.DefenderHealth, x => x.ToString("n0")),
            GetDiffSelector(currentEvent, previousEvent, e => e.DefenderStats.Strength, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.DefenderStats.Defence, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.DefenderStats.Speed, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.DefenderStats.Dexterity, NumberExtensions.ToSimpleString),
            currentEvent.Turn.ToString(),
        ];

        /*return [
            currentEvent.Turn.ToString(),
            GetSource(currentEvent),
            currentEvent.Type.ToString().ToColouredString(EventColours[currentEvent.Type]),
            currentEvent.Data.Format().ToColouredString(currentEvent.Source == PlayerType.Attacker ? "#C1E1C1" : "#FAA0A0"),
            GetDiffSelector(currentEvent, previousEvent, e => (ulong)e.AttackerHealth, x => x.ToString("n0")),
            GetDiffSelector(currentEvent, previousEvent, e => (ulong)e.DefenderHealth, x => x.ToString("n0")),
            GetDiffSelector(currentEvent, previousEvent, e => e.AttackerStats.Strength, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.AttackerStats.Defence, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.AttackerStats.Speed, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.AttackerStats.Dexterity, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.DefenderStats.Strength, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.DefenderStats.Defence, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.DefenderStats.Speed, NumberExtensions.ToSimpleString),
            GetDiffSelector(currentEvent, previousEvent, e => e.DefenderStats.Dexterity, NumberExtensions.ToSimpleString),
        ];*/
    }

    private string GetEventDetails(ThunderdomeEvent tEvent)
    {
        return $"{tEvent.Type} + " + string.Join(" ", tEvent.Data);
    }

    private string GetSource(ThunderdomeEvent tEvent)
    {
        if (tEvent.Source == 0)
        {
            return " ";
        }

        return tEvent.Source.ToString().ToColouredString(tEvent.Source == PlayerType.Attacker ? "#C1E1C1" : "#FAA0A0");
    }

    private string GetDiffSelector<T>(
        ThunderdomeEvent currentEvent,
        ThunderdomeEvent? previousEvent,
        Func<ThunderdomeEvent, T> selector,
        Func<T, string> formatter) where T : INumber<T>, IParsable<T>, IComparable<T>
    {
        if (previousEvent == null || currentEvent.Type == ThunderdomeEventType.FightEnd)
        {
            return formatter(selector.Invoke(currentEvent)!);
        }

        T currentValue = selector.Invoke(currentEvent)!;
        T previousValue = selector.Invoke(previousEvent)!;

        if (currentValue == previousValue)
        {
            return " ";
        }

        string colour = currentValue > previousValue
            ? "#77dd77"
            : "#ff7974";

        return formatter(currentValue).ToColouredString(colour);
    }

    private TableColumn DefaultColumn(string header)
    {
        return new TableColumn(header)
        {
            Padding = new Padding(3, 0),
            Alignment = Justify.Center
        };
    }
}