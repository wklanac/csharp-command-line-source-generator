using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using static System.Linq.Enumerable;
using JetBrains.Annotations;

namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public record CommandConfigNode(
    string Name,
    string Description,
    List<ArgumentConfigNode>? Arguments = null,
    List<OptionConfigNode>? Options = null,
    List<CommandConfigNode>? SubCommands = null)
: ICommandLineConfigNode, IChildConfig<CommandConfigNode>
{
    public string Name { get; } = Name;
    public string Description { get; } = Description;
    public List<ArgumentConfigNode> Arguments { get; } = Arguments ?? [];
    public List<OptionConfigNode> Options { get; } = Options ?? [];
    public List<CommandConfigNode> SubCommands { get; } = SubCommands ?? [];

    public void Accept(IConfigVisitor configVisitor)
    {
        configVisitor.Visit(this);
    }

    public IEnumerable<INode> GetChildren()
    {
        return ImmutableList.Create(
            SubCommands.OfType<INode>().Concat(Options).Concat(Arguments).ToArray());
    }

    public ICommandLineConfigRelationship GetParentRelationship(CommandConfigNode parent)
    {
        return new CommandSubcommandRelationship(parent, this);
    }
}