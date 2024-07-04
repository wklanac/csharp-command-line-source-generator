namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public static class NodeExtensions
{
    public static bool IsLeaf(this INode node)
    {
        return !node.GetChildren().Any();
    }
}