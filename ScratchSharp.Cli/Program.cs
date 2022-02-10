/*
using ScratchSharp;
using ScratchSharp.Binding;

while (true)
{
    var line = Console.ReadLine();
    var syntaxTree = SyntaxTree.Parse(line);
    var binder = new Binder();
    var boundExpression = binder.Bind(syntaxTree.Root);
    
    Console.ForegroundColor = ConsoleColor.DarkGray;
    TreeViewer.PrettyPrint(syntaxTree.Root);
    Console.ResetColor();
    
    var e = new Evaluator(boundExpression);
    Console.WriteLine(e.Evaluate());
}
*/

using ScratchSharp.Cli;
using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp();
app.SetDefaultCommand<InteractiveCommand>();
app.Configure(c =>
{
    c.AddCommand<NewCommand>("new")
        .WithDescription("Create a new ScratchSharp project.")
        .WithExample(new[] {"new", "TestProject"});
    c.AddCommand<ExplainCommand>("explain")
        .WithDescription("Need some help with ScratchSharp? Use this command.")
        .WithExample(new[] {"list"})
        .WithExample(new[] {"repl_modules"});
    c.AddCommand<ReplCommand>("repl")
        .WithDescription("Enter the ScratchSharp REPL.")
        .WithExample(new[] {"repl"});
});
app.Run(args);

public class InteractiveCommand : Command
{
    public override int Execute(CommandContext context)
    {
        AnsiConsole.MarkupLine("It seems that you ran the app without any arguments. You will enter [green]interactive mode[/].");
        var action = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("I would like to..")
            .PageSize(10)
            .AddChoices("Create a new project", "Enter the REPL", "Get some help with ScratchSharp", "Exit the app")
        );

        switch (action)
        {
            case "Create a new project":
                ProjectCreator.Interactive();
                break;
            case "Exit the app":
                AnsiConsole.MarkupLine("Thank you for using ScratchSharp!");
                Environment.Exit(0);
                break;
            case "Get some help with ScratchSharp":
                Explain.Interactive();
                break;
            case "Enter the REPL":
                Repl.StartRepl();
                break;
        }

        return 0;
    }
}

public class NewCommand : Command<NewCommand.NewCommandOptions>
{
    public class NewCommandOptions: CommandSettings
    {
        [CommandArgument(1, "[ProjectType]")]
        public string ProjectType { get; set; }
        
        [CommandArgument(0, "<ProjectName>")]
        public string ProjectName { get; set; }
        
    }

    public override int Execute(CommandContext context, NewCommandOptions settings)
    {
        ProjectCreator.Create(settings.ProjectName, settings.ProjectType);
        return 0;
    }
}

public class ExplainCommand: Command<ExplainCommand.ExplainCommandOptions>
{
    public class ExplainCommandOptions : CommandSettings
    {
        [CommandArgument(0, "<Type>")]
        public string Type { get; set; }
    }

    public override int Execute(CommandContext context, ExplainCommandOptions settings) => Explain.ExplainDisplay(settings.Type);
}

public class ReplCommand : Command
{
    public override int Execute(CommandContext context)
    {
        Repl.StartRepl();
        return 0;
    }
}