using System.Collections.Immutable;
using System.CommandLine;

namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public record ArgumentConfigNode(string Name, string Description, string TypeName,
    ArgumentArity Arity, object? DefaultValue)
: ICommandLineConfigNode, IChildConfig<CommandConfigNode>
{
    public string Name { get; } = Name;
    public string Description { get; } = Description;
    public string TypeName { get; } = TypeName;
    public ArgumentArity Arity { get; } = Arity;
    public object? DefaultValue { get; } = DefaultValue;
    
    public void Accept(IConfigVisitor configVisitor)
    {
        configVisitor.Visit(this);
    }

    public IEnumerable<INode> GetChildren()
    {
        return ImmutableList<INode>.Empty;
    }

    public ICommandLineConfigRelationship GetParentRelationship(CommandConfigNode parent)
    {
        return new CommandArgumentRelationship(parent, this);
    }
}