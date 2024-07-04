using CommandLineGenerator.ComponentGenerator.Static.Model;

namespace CommandLineGenerator.ComponentGenerator.Static;

/// <summary>
///     Extension methods for <see cref="INode" />
/// </summary>
public static class NodeExtensions
{
    public static bool IsLeaf(this INode node)
    {
        return !node.GetChildren().Any();
    }
}