using System.Collections.Immutable;
using System.CommandLine;

namespace CommandLineGenerator.ComponentGenerator.Static.Model;

/// <summary>
///     Option configuration model for <see cref="System.CommandLine.Option" />
/// </summary>
/// <param name="Name">Option name</param>
/// <param name="Description">Option description</param>
/// <param name="TypeName">Option type name</param>
/// <param name="OptionArity"><see cref="System.CommandLine.ArgumentArity" /> for option</param>
/// <param name="AllowMultipleArgumentsPerToken">If true, allow multiple arguments per option instance</param>
/// <param name="IsRequired">If true, this option must be supplied</param>
public record OptionConfigNode(
    string Name,
    string Description,
    string TypeName,
    ArgumentArity OptionArity,
    bool AllowMultipleArgumentsPerToken,
    bool IsRequired)
    : ICommandLineConfigNode, IChildConfig<CommandConfigNode>
{
    public string Description { get; } = Description;
    public string TypeName { get; } = TypeName;
    public ArgumentArity OptionArity { get; } = OptionArity;
    public bool IsRequired { get; } = IsRequired;
    public bool AllowMultipleArgumentsPerToken { get; } = AllowMultipleArgumentsPerToken;

    public ICommandLineConfigRelationship GetParentRelationship(CommandConfigNode parent)
    {
        return new CommandOptionRelationship(parent, this);
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