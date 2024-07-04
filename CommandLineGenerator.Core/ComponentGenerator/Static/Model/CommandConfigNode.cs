using System.Collections.Immutable;

namespace CommandLineGenerator.ComponentGenerator.Static.Model;

/// <summary>
///     Configuration node model of <see cref="System.CommandLine.Command" />
/// </summary>
/// <param name="Name">Command name</param>
/// <param name="Description">Command description</param>
/// <param name="Arguments">Command arguments, if required</param>
/// <param name="Options">Command options, if required</param>
/// <param name="SubCommands">Command subcommands, if required</param>
public record CommandConfigNode(
    string Name,
    string Description,
    List<ArgumentConfigNode>? Arguments = null,
    List<OptionConfigNode>? Options = null,
    List<CommandConfigNode>? SubCommands = null)
    : ICommandLineConfigNode, IChildConfig<CommandConfigNode>
{
    public string Description { get; } = Description;
    public List<ArgumentConfigNode> Arguments { get; } = Arguments ?? [];
    public List<OptionConfigNode> Options { get; } = Options ?? [];
    public List<CommandConfigNode> SubCommands { get; } = SubCommands ?? [];

    public ICommandLineConfigRelationship GetParentRelationship(CommandConfigNode parent)
    {
        return new CommandSubcommandRelationship(parent, this);
    }

    public string Name { get; } = Name;

    public void Accept(IConfigVisitor configVisitor)
    {
        configVisitor.Visit(this);
    }

    public IEnumerable<INode> GetChildren()
    {
        return ImmutableList.Create(
            SubCommands.OfType<INode>().Concat(Options).Concat(Arguments).ToArray());
    }
}