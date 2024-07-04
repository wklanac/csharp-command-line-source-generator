namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public interface ICommandLineConfigRelationship
{
    public void Accept(IConfigRelationshipVisitor configRelationshipVisitor);
    public ICommandLineConfig Parent { get; }
    public ICommandLineConfig Child { get; }
}