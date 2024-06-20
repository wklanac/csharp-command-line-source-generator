using System.CommandLine;
using System.Text;
using CommandLineGenerator.ComponentGeneration.Source;

namespace CommandLineGenerator.Testing.ComponentGeneration.Source;

public class ArgumentSourceGeneratorTest
{
    private static readonly Argument<int> argumentWithDescription = new (name: "someInput", description: "A description.");
    private static readonly Argument<string> argumentWithoutDescription = new(name: "anotherInput");

    private const string argumentWithDescriptionString = """
                                                         var someInputArgument = new Argument<System.Int32>(
                                                             name: "someInput",
                                                             description: "A description."
                                                         );
                                                         """;

    private const string argumentWithoutDescriptionString = """
                                                            var anotherInputArgument = new Argument<System.String>(
                                                                name: "anotherInput"
                                                            );
                                                            """;

    private static object[] generateSingleArgumentCases =
    [
        new object[]{ argumentWithDescription, argumentWithDescriptionString },
        new object[]{ argumentWithoutDescription, argumentWithoutDescriptionString }
    ];

    
    private ArgumentSourceGenerator cliArgumentGenerator;
    private StringBuilder stringBuilder;
    
    [SetUp]
    public void Setup()
    {
        stringBuilder = new StringBuilder();
        cliArgumentGenerator = new ArgumentSourceGenerator(stringBuilder);
    }

   
    [Test, TestCaseSource(nameof(generateSingleArgumentCases))]
    public void GenerateSingleArgument(Argument argument, String expectedResult)
    {
        // When: Requesting a single argument is generated.
        cliArgumentGenerator.GenerateAndAdd(argument);
        
        // Then: The output string matches the expected format.
        Assert.That(stringBuilder.ToString(), Is.EqualTo(expectedResult));
    }
}