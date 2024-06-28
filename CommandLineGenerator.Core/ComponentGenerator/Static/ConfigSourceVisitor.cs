using CommandLineGenerator.ComponentGenerator.Static.Model;
using CommandLineGenerator.SourceWriter;

namespace CommandLineGenerator.ComponentGenerator.Static;

public class ConfigSourceVisitor(ISourceWriter sourceWriter): IConfigVisitor
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
        sourceWriter.WriteLine($"{optionConfigNode.Name}Option.Arity = {optionConfigNode.OptionArity};");
        sourceWriter.WriteLine($"{optionConfigNode.Name}Option.IsRequired = {optionConfigNode.IsRequired};");
        sourceWriter.WriteLine($"{optionConfigNode.Name}Option.AllowMultipleArgumentsPerToken = "
                               + $"{optionConfigNode.AllowMultipleArgumentsPerToken};");
    }

    public void Visit(ArgumentConfigNode argumentConfigNode)
    {
        sourceWriter.WriteLine($"var {argumentConfigNode.Name}Argument = new Argument<{argumentConfigNode.TypeName}>(");
        sourceWriter.Indent();
        sourceWriter.WriteLine($"name: \"{argumentConfigNode.Name}\",");
        sourceWriter.WriteLine($"description: \"{argumentConfigNode.Description}\"");
        sourceWriter.Unindent();
        sourceWriter.WriteLine(");");
        sourceWriter.WriteLine($"{argumentConfigNode.Name}Argument.Arity = new ArgumentArity({argumentConfigNode.Arity.MinimumNumberOfValues},{argumentConfigNode.Arity.MaximumNumberOfValues});");
        if (argumentConfigNode.DefaultValue != null)
        {
            sourceWriter.WriteLine($"{argumentConfigNode.Name}Argument.SetDefaultValue({argumentConfigNode.DefaultValue});");
        }
    }
}