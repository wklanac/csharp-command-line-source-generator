namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public interface INode
{
    public IEnumerable<INode> GetChildren();
}