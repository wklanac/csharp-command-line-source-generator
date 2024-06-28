using System.Collections.Immutable;
using System.CommandLine;

namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public record OptionConfigNode(string Name, string Description, string TypeName,
    ArgumentArity OptionArity, bool AllowMultipleArgumentsPerToken, bool IsRequired)
    : ICommandLineConfigNode, IChildConfig<CommandConfigNode>
{
    public string Name { get; } = Name;
    public string Description { get; } = Description;
    public string TypeName { get; } = TypeName;
    public ArgumentArity OptionArity { get; } = OptionArity;
    public bool IsRequired { get; } = IsRequired;
    public bool AllowMultipleArgumentsPerToken { get; } = AllowMultipleArgumentsPerToken;
    
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
        return new CommandOptionRelationship(parent, this);
    }
}