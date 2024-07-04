using System.Collections.Immutable;

namespace CommandLineGenerator.ComponentGenerator.Static.Model;

/// <summary>
///     Tree model of nodes which are command line configuration.
///     Maintains state in the form of the most recent parent
///     traversed in the tree.
/// </summary>
/// <param name="rootConfigNode">Root configuration node for tree</param>
/// <typeparam name="T">Root node type</typeparam>
public class StatefulConfigTree<T>(T rootConfigNode)
    where T : ICommandLineConfig, INode
{
    private readonly Stack<T> _configNodesToProcess
        = new(ImmutableList.Create(rootConfigNode));

    private readonly Stack<Tuple<T, int>> _parentReferenceStack = new();

    public T MostRecentParent => _parentReferenceStack.Peek().Item1;
    public int ParentsTracked => _parentReferenceStack.Count;


    public IEnumerator<T> GetEnumerator()
    {
        while (_configNodesToProcess.Count > 0)
        {
            var currentNode = _configNodesToProcess.Pop();
            yield return currentNode;

            ManageParentReferencesForNode(currentNode);

            var nodeChildren = currentNode.GetChildren();
            foreach (var nodeChild in nodeChildren.Reverse()) _configNodesToProcess.Push((T)nodeChild);
        }
    }

    private void ManageParentReferencesForNode(T configNode)
    {
        if (!configNode.IsLeaf())
        {
            _parentReferenceStack.Push(new Tuple<T, int>(configNode, configNode.GetChildren().Count()));
        }
        else
        {
            var mostRecentParentReferences = _parentReferenceStack.Pop();
            var remainingChildrenToProcess = mostRecentParentReferences.Item2 - 1;
            if (remainingChildrenToProcess > 0)
                _parentReferenceStack.Push(new Tuple<T, int>(
                    mostRecentParentReferences.Item1,
                    remainingChildrenToProcess));
        }
    }
}