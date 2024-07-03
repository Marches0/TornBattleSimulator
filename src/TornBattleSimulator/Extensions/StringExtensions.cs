using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TornBattleSimulator.Extensions;
public static class StringExtensions
{
    public static string ToColouredString(this string str, string colour)
    {
        return $"[{colour}]{str}[/]";
    }
}