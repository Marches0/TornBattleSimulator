﻿using Spectre.Console;
using TornBattleSimulator.Battle.Thunderdome.Player;
using TornBattleSimulator.Extensions;

namespace TornBattleSimulator.Battle.Thunderdome.Output;

public class ThunderdomeResultWriter
{
    public void Write(ThunderdomeContext context)
    {
        Table table = new Table()
            .NoBorder()
            .LeftAligned()
            //.Centered()
            .Title($"{context.Attacker.Build.Name.EscapeMarkup()} attacking {context.Defender.Build.Name.EscapeMarkup()} ({context.GetResult()})");

        table.AddColumns("T", "Player", "Event", "Details", "ATT HP", "DEF HP");
        
        foreach(var col in table.Columns)
        {
            col.Padding = new Padding(2, 0);
        }

        foreach(var tEvent in context.Events)
        {
            table.AddRow(ToRow(tEvent));
        }

        AnsiConsole.Write(table);
    }

    private string[] ToRow(ThunderdomeEvent tEvent)
    {
        return [
            tEvent.Turn.ToString(),
            tEvent.Source.ToString().ToColouredString(tEvent.Source == PlayerType.Attacker ? "#C1E1C1" : "#FAA0A0"),
            tEvent.Type.ToString(),
            tEvent.Data.Format(),
            tEvent.AttackerHealth.ToString("n0"),
            tEvent.DefenderHealth.ToString("n0"),
        ];
    }

    private string GetEventDetails(ThunderdomeEvent tEvent)
    {
        return $"{tEvent.Type} + " + string.Join(" ", tEvent.Data);
    }
}