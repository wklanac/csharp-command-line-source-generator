using System.Collections.Immutable;
using System.CommandLine;

namespace CommandLineGenerator.ComponentGenerator.Static.Model;

/// <summary>
///     Configuration node model of <see cref="System.CommandLine.Argument" />
/// </summary>
/// <param name="Name">Argument name</param>
/// <param name="Description">Argument description</param>
/// <param name="TypeName">Name of type to use for argument creation</param>
/// <param name="Arity"><see cref="System.CommandLine.ArgumentArity" /> for argument creation</param>
/// <param name="DefaultValue">Default value to assign to argument</param>
public record ArgumentConfigNode(
    string Name,
    string Description,
    string TypeName,
    ArgumentArity Arity,
    object? DefaultValue)
    : ICommandLineConfigNode, IChildConfig<CommandConfigNode>
{
    public string Description { get; } = Description;
    public string TypeName { get; } = TypeName;
    public ArgumentArity Arity { get; } = Arity;
    public object? DefaultValue { get; } = DefaultValue;

    public ICommandLineConfigRelationship GetParentRelationship(CommandConfigNode parent)
    {
        return new CommandArgumentRelationship(parent, this);
    }

    public string Name { get; } = Name;

    public void Accept(IConfigVisitor configVisitor)
    {
        configVisitor.Visit(this);
    }

    public IEnumerable<INode> GetChildren()
    {
        return ImmutableList<INode>.Empty;
    }
}