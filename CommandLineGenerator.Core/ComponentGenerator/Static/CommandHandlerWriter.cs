using CommandLineGenerator.ComponentGenerator.Static.Model;
using CommandLineGenerator.Writer;

namespace CommandLineGenerator.ComponentGenerator.Static;

/// <summary>
///     Writer class for command handler wiring.
///     Assumes that for each command, a handler of the same name is implemented by the user.
/// </summary>
/// <param name="sourceWriter">Source writer to use for writing handler wiring</param>
public class CommandHandlerWriter(ISourceWriter sourceWriter)
{
    public void AddHandler(CommandConfigNode commandConfigNode)
    {
        sourceWriter.WriteLine($"{commandConfigNode.Name}Command.SetHandler(");
        sourceWriter.Indent();

        var optionNames = commandConfigNode.Options.Select(o => o.Name).ToList();
        var argumentNames = commandConfigNode.Arguments.Select(a => a.Name).ToList();

        var optionSymbolNames = optionNames.Select(on => on + "Option");
        var argumentSymbolNames = argumentNames.Select(on => on + "Argument");

        var allInputNames = string.Join(",", optionNames.Concat(argumentNames));
        var allInputSymbols = string.Join(",", optionSymbolNames.Concat(argumentSymbolNames));
        var handlerString = $"({allInputNames}) => {commandConfigNode.Name}({allInputNames}), {allInputSymbols}";

        sourceWriter.WriteLine(handlerString);

        sourceWriter.Unindent();
        sourceWriter.WriteLine(");");
    }
}