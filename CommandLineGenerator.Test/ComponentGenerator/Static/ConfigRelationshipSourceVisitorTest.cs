using System.Collections.Immutable;
using System.CommandLine;
using CommandLineGenerator.ComponentGenerator.Static;
using CommandLineGenerator.ComponentGenerator.Static.Model;
using CommandLineGenerator.SourceWriter;
using Moq;

namespace CommandLineGenerator.Testing.ComponentGenerator.Static;

public class ConfigRelationshipSourceVisitorTest
{
    private Mock<ISourceWriter> mockSourceWriter;
    private ConfigRelationshipSourceVisitor configRelationshipSourceVisitor;
    
    [SetUp]
    public void Setup()
    {
        mockSourceWriter = new Mock<ISourceWriter>();
        configRelationshipSourceVisitor = new ConfigRelationshipSourceVisitor(mockSourceWriter.Object);
    }
    
    [Test]
    public void TestVisitCommandSubcommand()
    {
        var commandSubcommandRelationship = new CommandSubcommandRelationship(
            new CommandConfigNode(
                "someCommand", "its description",
                ImmutableList<ArgumentConfigNode>.Empty.ToList(),
                ImmutableList<OptionConfigNode>.Empty.ToList(),
                ImmutableList<CommandConfigNode>.Empty.ToList()),
            new CommandConfigNode(
                "itsChildCommand", "its description",
                ImmutableList<ArgumentConfigNode>.Empty.ToList(),
                ImmutableList<OptionConfigNode>.Empty.ToList(),
                ImmutableList<CommandConfigNode>.Empty.ToList()));
        configRelationshipSourceVisitor.Visit(commandSubcommandRelationship);
        
        mockSourceWriter.Verify(
            writer => writer.WriteLine(
                It.IsRegex(@"\w*someCommand\w*\.AddCommand\(\w*itsChildCommand\w*\)")));
    }
    
    [Test]
    public void TestVisitCommandOption()
    {
        var commandSubcommandRelationship = new CommandOptionRelationship(
            new CommandConfigNode(
                "someCommand", 
                "its description",
                ImmutableList<ArgumentConfigNode>.Empty.ToList(),
                ImmutableList<OptionConfigNode>.Empty.ToList(),
                ImmutableList<CommandConfigNode>.Empty.ToList()),
            new OptionConfigNode(
                "someOption",
                "its description",
                "string",
                new ArgumentArity(1, 1),
                false,
                false));
        configRelationshipSourceVisitor.Visit(commandSubcommandRelationship);
        
        mockSourceWriter.Verify(
            writer => writer.WriteLine(
                It.IsRegex(@"\w*someCommand\w*\.AddOption\(\w*someOption\w*\)")));
    }
    
    [Test]
    public void TestVisitCommandArgument()
    {
        var commandSubcommandRelationship = new CommandArgumentRelationship(
            new CommandConfigNode(
                "someCommand",
                "its description",
                ImmutableList<ArgumentConfigNode>.Empty.ToList(),
                ImmutableList<OptionConfigNode>.Empty.ToList(),
                ImmutableList<CommandConfigNode>.Empty.ToList()),
            new ArgumentConfigNode(
                "someArgument",
                "its description",
                "string",
                new ArgumentArity(1, 1),
                "defaultValue"));
        configRelationshipSourceVisitor.Visit(commandSubcommandRelationship);
        
        mockSourceWriter.Verify(
            writer => writer.WriteLine(
                It.IsRegex(@"\w*someCommand\w*\.AddArgument\(\w*someArgument\w*\)")));
    }
}