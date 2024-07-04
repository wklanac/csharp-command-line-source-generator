namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public interface ICommandLineConfig
{
    public void Accept(IConfigVisitor configVisitor);
    public string Name { get; }
}