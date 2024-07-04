namespace CommandLineGenerator.ComponentGenerator.Static.Model;

/// <summary>
///     Interface describing a parent child relationship between
///     a parent and child configuration visitee, which is itself a visitee
/// </summary>
public interface ICommandLineConfigRelationship
{
    public ICommandLineConfig Parent { get; }
    public ICommandLineConfig Child { get; }
    public void Accept(IConfigRelationshipVisitor configRelationshipVisitor);
}