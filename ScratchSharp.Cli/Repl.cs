using System.Text.RegularExpressions;
using Spectre.Console;
using ScratchSharp.Core;
using ScratchSharp.Core.Syntax;

namespace ScratchSharp.Cli;

public class Repl
{
    public static void StartRepl()
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("Welcome to the [green]ScratchSharp REPL[/]. You can evaluate basic code here, but you [bold italic red]cannot import packages[/].\nTo read more about modules, execute [yellow]\"ScratchSharp explain repl_modules\"[/].\nTo learn about REPL-specific commands, enter [yellow]\".commands\"[/].");
        var variables = new Dictionary<string, object>();
        var showTree = false;
        while (true)
        {
            var code = AnsiConsole.Ask<string>(">");
            switch (code)
            {
                case ".exit":
                    AnsiConsole.MarkupLine("Thank you for using ScratchSharp!");
                    Environment.Exit(0);
                    break;
                case ".showTree":
                {
                    showTree = !showTree;
                    AnsiConsole.MarkupLine($"[italic yellow]{(showTree ? "Now showing syntax trees.": "Now hiding syntax trees.")}[/]");
                    break;
                }
                default:
                    if (code.StartsWith("."))
                    {
                        AnsiConsole.MarkupLine("[bold red]Unknown REPL command.[/]");
                        break;
                    }
                    ExceptionWrapper.SafelyRunCode(code, variables, showTree);
                    break;
            }
        }
    }
}