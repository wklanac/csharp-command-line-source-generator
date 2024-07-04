using System.Collections.Immutable;
using CommandLineGenerator.ComponentGenerator.Static;
using CommandLineGenerator.ComponentGenerator.Static.Model;
using CommandLineGenerator.SourceWriter;
using Moq;

namespace CommandLineGenerator.Testing.ComponentGenerator.Static;

public class CommandHandlerWriterTest
{
    [Test]
    public void TestHandleCommandConfig()
    {
        var commandConfig = new CommandConfigNode("someCommand", "A description of functionality.",
            ImmutableList<ArgumentConfigNode>.Empty.ToList(),
            ImmutableList<OptionConfigNode>.Empty.ToList(),
            ImmutableList<CommandConfigNode>.Empty.ToList());

        var mockSourceWriter = new Mock<ISourceWriter>();
        
        var commandHandlerWriter = new CommandHandlerWriter(mockSourceWriter.Object);
        
        commandHandlerWriter.AddHandler(commandConfig);
        
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsAny<string>()), Times.AtLeast(3));
        mockSourceWriter.Verify(writer => writer.Indent(), Times.Exactly(1));
        mockSourceWriter.Verify(writer => writer.Unindent(), Times.Exactly(1));
    }
}