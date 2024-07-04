using System.CommandLine;
using CommandLineGenerator.ComponentGenerator.Static;
using CommandLineGenerator.ComponentGenerator.Static.Model;
using CommandLineGenerator.SourceWriter;
using Newtonsoft.Json;

namespace CommandLineGenerator;

using Microsoft.CodeAnalysis;

[Generator]
public class CommandLineSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context) 
    {
        // No initialization required for this generator
    }

    public void Execute(GeneratorExecutionContext context)
    {
        // Read JSON Configuration and raise exceptions for null output failure modes
        var configText = context.AdditionalFiles.FirstOrDefault(f => f.Path.EndsWith("commands"))?.GetText();
        if (configText == null)
        {
            throw new ArgumentException("Cannot find a 'commands.json' file to use in the source generator context.");
        }

        var config = JsonConvert.DeserializeObject<RootCommandConfigNode>(configText.ToString());
             
        if (config == null)
        {
            throw new Exception("Unknown error occurred during command configuration deserialization.");
        }
        
        // Find the most likely namespace for the Program type symbol to exist in given the default
        // CLI .NET project structure
        var likelyProgramTypeNamespace =
            context.Compilation
                .GetSymbolsWithName(candidate => candidate.Equals("Program"), SymbolFilter.Type)
                .First()
                .ContainingNamespace
                .Name;
        
        // Create source writer and establish file head contents
        var sourceWriter = new DefaultSourceWriter(new DefaultSourceWriterSettings());
        sourceWriter.WriteLine($"namespace {likelyProgramTypeNamespace};");
        sourceWriter.WriteLine("");
        sourceWriter.WriteLine("using System.CommandLine;");
        sourceWriter.WriteLine("using System.Threading.Tasks;");
        sourceWriter.WriteLine("");
        sourceWriter.OpenBlock("public partial class Program");
        sourceWriter.OpenBlock("public async static Task<int> InvokeFrontend(string[] args)");
        
        // Iterate over root command and visit each configuration node with a source visitor,
        // tracking relationships along the way
        var statefulConfigTree = new StatefulConfigTree<ICommandLineConfigNode>(config);
        var configSourceVisitor = new ConfigSourceVisitor(sourceWriter);
        var commandConfigRelationships = new List<ICommandLineConfigRelationship>();
        var commandsToWriteHandlersFor = new List<CommandConfigNode>();
        
        foreach (var configNode in statefulConfigTree)
        {
            configNode.Accept(configSourceVisitor);
            if (configNode is CommandConfigNode knownCommand && statefulConfigTree.ParentsTracked > 0)
            {
                commandsToWriteHandlersFor.Add(knownCommand);
            }
            
            if (configNode is not IChildConfig<CommandConfigNode> knownChild
                || statefulConfigTree.ParentsTracked == 0)
            {
                continue;
            }
            
            var mostRecentParent = statefulConfigTree.MostRecentParent;
            var relationship = knownChild.GetParentRelationship((CommandConfigNode)mostRecentParent);
            commandConfigRelationships.Add(relationship);
        }

        // Iterate over the known parent child relationships in the command tree structure
        var configRelationshipSourceVisitor = new ConfigRelationshipSourceVisitor(sourceWriter);
        commandConfigRelationships.ForEach(
            ccr => ccr.Accept(configRelationshipSourceVisitor));
        
        // Iterate over known commands to establish handlers
        var commandHandlerWriter = new CommandHandlerWriter(sourceWriter);
        commandsToWriteHandlersFor.ForEach(c => commandHandlerWriter.AddHandler(c));
        
        // Invoke root command before closing blocks
        sourceWriter.WriteLine("return await rootCommand.InvokeAsync(args);");
        
        // Close method and partial class definition
        sourceWriter.CloseBlock("");
        sourceWriter.CloseBlock("");
        
        // Add source file from source writer contents
        context.AddSource("Program.g.cs", sourceWriter.ToString());
    }
    
}