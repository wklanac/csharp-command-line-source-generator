using System.Collections;
using System.Collections.Immutable;

namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public class StatefulConfigTree<T>(T rootConfigNode)
    where T: ICommandLineConfig, INode
{
    private readonly Stack<T> configNodesToProcess 
        = new (ImmutableList.Create(rootConfigNode));
    private readonly Stack<Tuple<T, int>> parentReferenceStack = new();

    public T MostRecentParent => parentReferenceStack.Peek().Item1;
    public int ParentsTracked => parentReferenceStack.Count;


    public IEnumerator<T> GetEnumerator()
    {
        while (configNodesToProcess.Count > 0)
        {
            var currentNode = configNodesToProcess.Pop();
            yield return currentNode;

            ManageParentReferencesForNode(currentNode);
            
            var nodeChildren = currentNode.GetChildren();
            foreach (var nodeChild in nodeChildren.Reverse())
            {
                configNodesToProcess.Push((T)nodeChild);
            }
        }
    }

    private void ManageParentReferencesForNode(T configNode)
    {
        if (!configNode.IsLeaf())
        {
            parentReferenceStack.Push(new Tuple<T, int>(configNode, configNode.GetChildren().Count()));
        }
        else
        {
            var mostRecentParentReferences = parentReferenceStack.Pop();
            var remainingChildrenToProcess = mostRecentParentReferences.Item2 - 1;
            if (remainingChildrenToProcess > 0)
            {
                parentReferenceStack.Push(new Tuple<T, int>(
                    mostRecentParentReferences.Item1,
                    remainingChildrenToProcess));
            }
        }
    }
}