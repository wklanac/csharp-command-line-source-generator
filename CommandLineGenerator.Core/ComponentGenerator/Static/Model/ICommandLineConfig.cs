namespace CommandLineGenerator.ComponentGenerator.Static.Model;

/// <summary>
///     Interface representing visited command line configurations
/// </summary>
public interface ICommandLineConfig
{
    public string Name { get; }
    public void Accept(IConfigVisitor configVisitor);
}