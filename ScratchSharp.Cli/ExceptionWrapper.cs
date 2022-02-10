using ScratchSharp.Core;
using ScratchSharp.Core.Syntax;
using Spectre.Console;

namespace ScratchSharp.Cli;

public class ExceptionWrapper
{
    public static void SafelyRunCode(string line, Dictionary<string, object> variables, bool showTree)
    {
        try
        {
            var tree = SyntaxTree.Parse(line);
            var result = new Compilation(tree).Evaluate(variables);
            if (showTree)
                TreeHelper.PrintTree(tree);
            AnsiConsole.MarkupLine($"[italic]{result}[/]");
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[red italic]Error running code: {e.Message}[/]");
            var show = AnsiConsole.Confirm("[yellow]Would you like to see the full exception?[/]");
            if(show)
                AnsiConsole.WriteException(e);
        }
    }
}