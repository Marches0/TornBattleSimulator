using Spectre.Console;

namespace TornBattleSimulator.Battle.Thunderdome.Output;

public class ThunderdomeResultWriter
{
    public void Write(ThunderdomeContext context)
    {
        Table table = new Table()
            .NoBorder()
            .LeftAligned()
            .Title($"{context.Attacker.Build.Name.EscapeMarkup()} attacking {context.Defender.Build.Name.EscapeMarkup()} ({context.GetResult()})");

        table.AddColumns("Turn", "Player", "Event", "Details");
        
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
            tEvent.Source.ToString(),
            tEvent.Type.ToString(),
            tEvent.Data.Format()
        ];
    }

    private string GetEventDetails(ThunderdomeEvent tEvent)
    {
        return $"{tEvent.Type} + " + string.Join(" ", tEvent.Data);
    }
}