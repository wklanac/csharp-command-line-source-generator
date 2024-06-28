namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public record CommandOptionRelationship(CommandConfigNode Parent, OptionConfigNode Child)
    : ICommandLineConfigRelationship
{
    public CommandConfigNode Parent { get; } = Parent;
    public OptionConfigNode Child { get; } = Child;

    public void Accept(IConfigRelationshipVisitor configRelationshipVisitor)
    {
        configRelationshipVisitor.Visit(this);
    }
}