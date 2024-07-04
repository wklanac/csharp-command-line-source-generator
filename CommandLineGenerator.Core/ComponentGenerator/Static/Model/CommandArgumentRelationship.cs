namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public record CommandArgumentRelationship : ICommandLineConfigRelationship
{
    public CommandArgumentRelationship(CommandConfigNode Parent, ArgumentConfigNode Child)
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