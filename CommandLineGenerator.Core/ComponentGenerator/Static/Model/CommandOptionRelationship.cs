namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public record CommandOptionRelationship : ICommandLineConfigRelationship
{
    public CommandOptionRelationship(CommandConfigNode Parent, OptionConfigNode Child)
    {
        this.Parent = Parent;
        this.Child = Child;
    }
    
    public ICommandLineConfig Parent { get; }
    public ICommandLineConfig Child { get; }

    public void Accept(IConfigRelationshipVisitor configRelationshipVisitor)
    {
        configRelationshipVisitor.Visit(this);
    }
}