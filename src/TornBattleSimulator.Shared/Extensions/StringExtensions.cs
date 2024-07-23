namespace TornBattleSimulator.Shared.Extensions;

public static class StringExtensions
{
    public static string ToColouredString(this string str, string colour)
    {
        return $"[{colour}]{str}[/]";
    }
}