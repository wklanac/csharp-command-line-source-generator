using System.CommandLine;
using CommandLineGenerator.ComponentGenerator.Static.Model;
using CommandLineGenerator.Utility;
using CommandLineGenerator.Writer;

namespace CommandLineGenerator.ComponentGenerator.Static;

/// <summary>
///     Configuration visitor to use for writing source code.
/// </summary>
/// <param name="sourceWriter">Source writer to use</param>
public class ConfigSourceVisitor(ISourceWriter sourceWriter) : IConfigVisitor
{
    public void Visit(RootCommandConfigNode rootCommandConfigNode)
    {
        sourceWriter.WriteLine($"var rootCommand = new RootCommand(\"{rootCommandConfigNode.Description}\");");
    }

    public void Visit(CommandConfigNode commandConfigNode)
    {
        sourceWriter.WriteLine($"var {commandConfigNode.Name}Command = new Command(");
        sourceWriter.Indent();
        sourceWriter.WriteLine($"name: \"{commandConfigNode.Name}\",");
        sourceWriter.WriteLine($"description: \"{commandConfigNode.Description}\"");
        sourceWriter.Unindent();
        sourceWriter.WriteLine(");");
    }

    public void Visit(OptionConfigNode optionConfigNode)
    {
        sourceWriter.WriteLine($"var {optionConfigNode.Name}Option = new Option<{optionConfigNode.TypeName}>(");
        sourceWriter.Indent();
        sourceWriter.WriteLine($"name: \"{optionConfigNode.Name}\",");
        sourceWriter.WriteLine($"description: \"{optionConfigNode.Description}\"");
        sourceWriter.Unindent();
        sourceWriter.WriteLine(");");
        sourceWriter.WriteLine(
            $"{optionConfigNode.Name}Option.Arity = new {ArgumentArityConstructor(optionConfigNode.OptionArity)};");
        sourceWriter.WriteLine(
            $"{optionConfigNode.Name}Option.IsRequired = {optionConfigNode.IsRequired.ToLiteral()};");
        sourceWriter.WriteLine($"{optionConfigNode.Name}Option.AllowMultipleArgumentsPerToken = "
                               + $"{optionConfigNode.AllowMultipleArgumentsPerToken.ToLiteral()};");
    }

    public void Visit(ArgumentConfigNode argumentConfigNode)
    {
        sourceWriter.WriteLine($"var {argumentConfigNode.Name}Argument = new Argument<{argumentConfigNode.TypeName}>(");
        sourceWriter.Indent();
        sourceWriter.WriteLine($"name: \"{argumentConfigNode.Name}\",");
        sourceWriter.WriteLine($"description: \"{argumentConfigNode.Description}\"");
        sourceWriter.Unindent();
        sourceWriter.WriteLine(");");
        sourceWriter.WriteLine(
            $"{argumentConfigNode.Name}Argument.Arity = new {ArgumentArityConstructor(argumentConfigNode.Arity)};");
        if (argumentConfigNode.DefaultValue != null)
            sourceWriter.WriteLine(
                $"{argumentConfigNode.Name}Argument.SetDefaultValue({argumentConfigNode.DefaultValue.ToLiteral()});");
    }

    private static string ArgumentArityConstructor(ArgumentArity argumentArity)
    {
        return $"ArgumentArity({argumentArity.MinimumNumberOfValues}, {argumentArity.MaximumNumberOfValues})";
    }
}