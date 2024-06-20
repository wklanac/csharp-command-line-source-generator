using System.CommandLine;
using System.Text;

namespace CommandLineGenerator.ComponentGeneration.Source;

public class ArgumentSourceGenerator(StringBuilder stringBuilder): IComponentGenerator<Argument>
{
    private StringBuilder stringBuilder = stringBuilder;
    
    public void GenerateAndAdd(Argument entity)
    {
        List<string> argumentDeclarationLines =
        [
            $"var {entity.Name}Argument = new Argument<{entity.ValueType}>(",
            $"    name: \"{entity.Name}\"{(string.IsNullOrEmpty(entity.Description) ? "" : ",")}"
        ];

        if (!string.IsNullOrEmpty(entity.Description))
        {
            argumentDeclarationLines.Add($"    description: \"{entity.Description}\"");
        }
        
        argumentDeclarationLines.Add(");");

        stringBuilder.AppendJoin("\n", argumentDeclarationLines);
    }
}