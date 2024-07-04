using System.CommandLine;
using CommandLineGenerator.ComponentGenerator.Static;
using CommandLineGenerator.ComponentGenerator.Static.Model;
using CommandLineGenerator.SourceWriter;
using Moq;

namespace CommandLineGenerator.Testing.ComponentGenerator.Static;

public class ConfigSourceVisitorTest
{
    private ConfigSourceVisitor configSourceVisitor;
    private Mock<ISourceWriter> mockSourceWriter;
    
    [SetUp]
    public void Setup()
    {
        mockSourceWriter = new Mock<ISourceWriter>();
        configSourceVisitor = new ConfigSourceVisitor(mockSourceWriter.Object);
    }

    [Test]
    public void TestVisitCommand()
    {
        var commandConfigNode = new CommandConfigNode(
            "print",
            "Print file content"
        );
        
        configSourceVisitor.Visit(commandConfigNode);
        
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex(@"var \w+ = new Command\(")));
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex("name: \"print\",")));
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex("description: \"Print file content\"")));
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex("\\);")));
    }

    [Test]
    public void TestVisitOption()
    {
        var optionConfigNode = new OptionConfigNode(
            "format",
            "Format to use for printing.",
            "string",
            new ArgumentArity(1, 1),
            false,
            false);
        
        configSourceVisitor.Visit(optionConfigNode);
        
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex(@"var \w+ = new Option<\w+>\(")));
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex("name: \"format\",")));
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex("description: \"Format to use for printing.\"")));
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex("\\);")));
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex(@"\w+.Arity = new ArgumentArity\(\d, \d\);")));
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex(@"\w+.IsRequired = (false|true)")));
        mockSourceWriter.Verify(
            writer => writer.WriteLine(It.IsRegex(@"\w+.AllowMultipleArgumentsPerToken = (false|true);")));
    }

    [Test]
    public void TestVisitArgument()
    {
        var argumentConfigNode = new ArgumentConfigNode(
            "file",
            "File to target for printing.",
            "string",
            new ArgumentArity(1, 1),
            "/opt/fileprintercli/demo.txt");
        
        configSourceVisitor.Visit(argumentConfigNode);
        
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex(@"var \w+ = new Argument<\w+>\(")));
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex("name: \"file\",")));
        mockSourceWriter.Verify(
            writer => writer.WriteLine(It.IsRegex("description: \"File to target for printing.\"")));
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex("\\);")));
        mockSourceWriter.Verify(writer => writer.WriteLine(It.IsRegex(@"\w+.Arity = new ArgumentArity\(\d, \d\);")));
        mockSourceWriter.Verify(
            writer => writer.WriteLine(It.IsRegex("\\w+\\.SetDefaultValue\\(\"/opt/fileprintercli/demo.txt\"\\);")));
    }
}