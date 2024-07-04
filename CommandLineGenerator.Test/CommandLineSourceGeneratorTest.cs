using System.Text;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

namespace CommandLineGenerator.Testing;

using Verifier = CSharpSourceGeneratorVerifier<CommandLineSourceGenerator>;

public class CommandLineSourceGeneratorTest
{
    [Test]
    public async Task TestSimpleCommandConfiguration()
    {
        const string code = """
                            namespace UnitTestCatCLI;

                            using System.Threading.Tasks;

                            public partial class Program
                            {
                                public async static Task<int> Main(string[] args)
                                {
                                    return await InvokeFrontend(args);
                                }
                            
                                public static void cat(string filePath)
                                {
                                    return;
                                }
                            }
                            """;
        const string generated = """
                                 namespace UnitTestCatCLI;

                                 using System.CommandLine;
                                 using System.Threading.Tasks;

                                 public partial class Program
                                 {
                                     public async static Task<int> InvokeFrontend(string[] args)
                                     {
                                         var rootCommand = new RootCommand("File command line tool");
                                         var catCommand = new Command(
                                             name: "cat",
                                             description: "Sequential file read"
                                         );
                                         var filePathArgument = new Argument<string>(
                                             name: "filePath",
                                             description: "File path to perform a sequential read on"
                                         );
                                         filePathArgument.Arity = new ArgumentArity(1, 1);
                                         rootCommand.AddCommand(catCommand);
                                         catCommand.AddArgument(filePathArgument);
                                         catCommand.SetHandler(
                                             (filePath) => cat(filePath), filePathArgument
                                         );
                                         return await rootCommand.InvokeAsync(args);
                                         
                                     }
                                     
                                 }

                                 """;

        const string jsonSource = """
                                  {
                                     "Description": "File command line tool",
                                     "SubCommands": [
                                         {
                                             "Name": "cat",
                                             "Description": "Sequential file read",
                                             "Arguments": [
                                                 {
                                                     "Name": "filePath",
                                                     "Description": "File path to perform a sequential read on",
                                                     "TypeName": "string",
                                                     "Arity": {
                                                         "minimumNumberOfValues": 1,
                                                         "maximumNumberOfValues": 1
                                                     }
                                                 }
                                             ]
                                         }
                                     ]
                                  }
                                  """;

        await new Verifier.Test
        {
            TestState =
            {
                Sources = { code },
                AdditionalFiles = { ("commands", jsonSource) },
                GeneratedSources =
                {
                    (typeof(CommandLineSourceGenerator),
                        "Program.g.cs",
                        SourceText.From(generated, Encoding.UTF8))
                }
            },
            ReferenceAssemblies = ReferenceAssemblies.Default
                .AddPackages([new PackageIdentity("System.CommandLine", "2.0.0-beta4.22272.1")])
        }.RunAsync();
    }
}