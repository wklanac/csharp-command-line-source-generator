using System.CommandLine;
using System.IO.Enumeration;
using System.Text;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

namespace CommandLineGenerator.Testing;

using Verifier = CSharpSourceGeneratorVerifier<CommandLineSourceGenerator>;

public class CommandLineSourceGeneratorTest
{
    [Test]
    public async Task Test1()
    {
        var code = "using System;";
        var generated = "using System;";

        var jsonSource = """
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
                                            "ArgumentArity": {
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
                        SourceText.From(generated, Encoding.UTF8, SourceHashAlgorithm.Sha256)),
                }
            },
            
            
        }.RunAsync();
    }
}