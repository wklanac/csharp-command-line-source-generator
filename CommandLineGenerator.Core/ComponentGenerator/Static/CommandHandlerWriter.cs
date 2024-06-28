using CommandLineGenerator.ComponentGenerator.Static.Model;
using CommandLineGenerator.SourceWriter;

namespace CommandLineGenerator.ComponentGenerator.Static;

public class CommandHandlerWriter(ISourceWriter sourceWriter)
{
    public void AddHandler(CommandConfigNode commandConfigNode)
    {
        sourceWriter.WriteLine($"{commandConfigNode.Name}Command.SetHandler(");
        
        var optionNames = commandConfigNode.Options.Select(o => o.Name).ToList();
        var argumentNames = commandConfigNode.Arguments.Select(a => a.Name).ToList();

        var optionSymbolNames = optionNames.Select(on => on + "Option");
        var argumentSymbolNames = argumentNames.Select(on => on + "Argument");
        
        var allInputNames = string.Join(",", optionNames.Concat(argumentNames));
        var allInputSymbols = string.Join(",", optionSymbolNames.Concat(argumentSymbolNames));
        var handlerString = $"({allInputNames}) => {commandConfigNode.Name}({allInputNames}), {allInputSymbols}";
        
        sourceWriter.WriteLine(handlerString);
        sourceWriter.WriteLine(");");
    }
}