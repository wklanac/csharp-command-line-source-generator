namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public record CommandArgumentRelationship(CommandConfigNode Parent, ArgumentConfigNode Child)
    : ICommandLineConfigRelationship
{
    public CommandConfigNode Parent { get; } = Parent;
    public ArgumentConfigNode Child { get; } = Child;

    public void Accept(IConfigRelationshipVisitor configRelationshipVisitor)
    {
        configRelationshipVisitor.Visit(this);
    }
}