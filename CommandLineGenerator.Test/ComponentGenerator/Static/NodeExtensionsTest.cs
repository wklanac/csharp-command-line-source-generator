using CommandLineGenerator.ComponentGenerator.Static;
using CommandLineGenerator.ComponentGenerator.Static.Model;
using Moq;

namespace CommandLineGenerator.Testing.ComponentGenerator.Static;

public class NodeExtensionsTest
{
    [TestCase(0, ExpectedResult = true)]
    [TestCase(3, ExpectedResult = false)]
    public bool TestIsLeaf(int mockedChildCount)
    {
        var mockChildList = new List<Mock<INode>>();
        for (var childIndex = 0; childIndex < mockedChildCount; childIndex++) mockChildList.Add(new Mock<INode>());

        var mockNode = new Mock<INode>();
        mockNode.Setup(node => node.GetChildren()).Returns(mockChildList.Select(mc => mc.Object));

        return mockNode.Object.IsLeaf();
    }
}