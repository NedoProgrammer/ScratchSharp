using Spectre.Console;

namespace ScratchSharp.Cli;

public class ProjectCreator
{
    public static void Create(string name, string type)
    {
        var progress = AnsiConsole.Progress()
            .HideCompleted(false)
            .AutoClear(true)
            .AutoRefresh(true);
        progress.Start(x =>
        {
            var create = x.AddTask("Create project directory")
                .IsIndeterminate();

            Directory.CreateDirectory($"{Environment.CurrentDirectory}/{name}");

            create.Description = "Create a hello world file";
            File.WriteAllText($"{Environment.CurrentDirectory}/{name}/main.scs", "import \"std/looks\";\nsay(\"hello, world!\");");
            
            create.StopTask();
        });
    }

    public static void Interactive()
    {
        AnsiConsole.Clear();
        var name = AnsiConsole.Prompt(new TextPrompt<string>("What is the name of your project?")
            .Validate(s => !string.IsNullOrEmpty(s) && !s.Contains(' '), "Project name cannot contain spaces!"));
        var type = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("What is the type of your project?")
            .PageSize(10)
            .AddChoices("Typical Scratch project"));
        Create(name, type);
    }
}