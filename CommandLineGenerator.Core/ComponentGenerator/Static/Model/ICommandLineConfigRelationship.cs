namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public interface ICommandLineConfigRelationship
{
    public void Accept(IConfigRelationshipVisitor configRelationshipVisitor);
}