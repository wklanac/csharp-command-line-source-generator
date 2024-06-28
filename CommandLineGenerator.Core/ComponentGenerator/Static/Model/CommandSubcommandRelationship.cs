namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public record CommandSubcommandRelationship(CommandConfigNode Parent, CommandConfigNode Child)
    : ICommandLineConfigRelationship
{
    public CommandConfigNode Parent { get; } = Parent;
    public CommandConfigNode Child { get; } = Child;

    public void Accept(IConfigRelationshipVisitor configRelationshipVisitor)
    {
        configRelationshipVisitor.Visit(this);
    }
}