namespace CommandLineGenerator.ComponentGenerator.Static.Model;

/// <summary>
///     Configuration node representing <see cref="System.CommandLine.RootCommand" />
/// </summary>
/// <param name="Description">Root command description</param>
/// <param name="Arguments">Root command arguments</param>
/// <param name="Options">Root command options</param>
/// <param name="SubCommands">Root command subcommands</param>
public record RootCommandConfigNode(
    string Description,
    List<ArgumentConfigNode> Arguments,
    List<OptionConfigNode> Options,
    List<CommandConfigNode> SubCommands) :
    CommandConfigNode("root", Description, Arguments, Options, SubCommands), ICommandLineConfig
{
    public new void Accept(IConfigVisitor configVisitor)
    {
        configVisitor.Visit(this);
    }
}