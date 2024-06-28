namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public static class NodeExtensionMethods
{
    public static bool IsLeaf(this INode node)
    {
        return !node.GetChildren().Any();
    }
}