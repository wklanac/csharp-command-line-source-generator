namespace CommandLineGenerator.ComponentGenerator.Static.Model;

/// <summary>
///     Interface composed of both a node interface - which is structural - and
///     a command line configuration interface - which is behavioural
/// </summary>
public interface ICommandLineConfigNode : INode, ICommandLineConfig
{
}