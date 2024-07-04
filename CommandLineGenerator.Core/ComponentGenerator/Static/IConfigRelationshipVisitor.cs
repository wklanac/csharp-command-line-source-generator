using CommandLineGenerator.ComponentGenerator.Static.Model;

namespace CommandLineGenerator.ComponentGenerator.Static;

/// <summary>
///     Visitor interface for configuration relationship types.
/// </summary>
public interface IConfigRelationshipVisitor
{
    public void Visit(CommandSubcommandRelationship commandSubcommandRelationship);
    public void Visit(CommandOptionRelationship commandOptionRelationship);
    public void Visit(CommandArgumentRelationship commandArgumentRelationship);
}