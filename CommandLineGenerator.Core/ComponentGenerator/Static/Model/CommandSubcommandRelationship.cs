namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public record CommandSubcommandRelationship : ICommandLineConfigRelationship
{
    public CommandSubcommandRelationship(CommandConfigNode Parent, CommandConfigNode Child)
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