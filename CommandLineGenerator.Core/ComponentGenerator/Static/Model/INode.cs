namespace CommandLineGenerator.ComponentGenerator.Static.Model;

/// <summary>
///     Interface describing a node suitable for use in hierarchical
///     or tree-like structures.
/// </summary>
public interface INode
{
    public IEnumerable<INode> GetChildren();
}