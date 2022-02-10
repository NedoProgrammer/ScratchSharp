using Spectre.Console;

namespace ScratchSharp.Cli;

public class Explain
{
    public static readonly Dictionary<string, string> Quotes = new()
    {
        {"repl_modules", @"
As you saw in the REPL, you [bold red italic]cannot import ScratchSharp packages[/] there.
Why? Well, while ScratchSharp [italic]does[/] get parsed as a regular programming language, the so-to-say [yellow italic]compiler[/]
will construct actual Scratch blocks, and you can't execute them in the REPL :(
However, since Scratch is open-source and MIT licensed, maybe one day there will be an interface for me to [italic]secretly[/] run
Scratch blocks, even if it will be time-consuming.
Also, in future releases of ScratchSharp there might be a new mode for running one-line commands: the [bold green]""interactive""[/] mode.
Once you enter a line of code, it gets parsed and pushed into the already open Scratch project, where it is displayed. But that's not the case right now.
"}
    };

    public static readonly Dictionary<string, string> Descriptions = new()
    {
        {"repl_modules", "Why can't I use modules in REPL?"}
    };

    public static int ExplainDisplay(string type)
    {
        if (type == "list")
        {
            List();
            return 0;
        }
        
        if (!Quotes.ContainsKey(type))
        {
            AnsiConsole.MarkupLine($"[bold red]Unknown quote \"{type}\".[/]\nPlease take a look at [italic]ScratchSharp explain list[/].");
            return 1;
        }
        AnsiConsole.MarkupLine(Quotes[type]);
        AnsiConsole.Markup("[italic]Press any key to exit.[/]");
        Console.ReadKey(true);
        AnsiConsole.WriteLine("\nThank you for using ScratchSharp!");
        Environment.Exit(0);
        return 0;
    }

    public static void Interactive()
    {
        AnsiConsole.Clear();
        var strings = Descriptions.Select(str => $"{str.Key}: {str.Value}").ToList();

        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .PageSize(10)
            .AddChoices(strings));
        ExplainDisplay(choice.Split(":")[0].Trim());
    }

    public static void List()
    {
        var table = new Table()
            .RoundedBorder();
        table.AddColumn("Name");
        table.AddColumn("Description");
        foreach (var (key, value) in Descriptions)
            table.AddRow(key, value);
        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }
}