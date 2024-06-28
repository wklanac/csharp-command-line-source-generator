using CommandLineGenerator.ComponentGenerator.Static.Model;
using CommandLineGenerator.SourceWriter;

namespace CommandLineGenerator.ComponentGenerator.Static;

public class ConfigRelationshipSourceVisitor(ISourceWriter sourceWriter): IConfigRelationshipVisitor
{
    public void Visit(CommandSubcommandRelationship commandSubcommandRelationship)
    {
        var parentName = commandSubcommandRelationship.Parent.Name;
        var childName = commandSubcommandRelationship.Child.Name;
        var relationshipSource = $"{parentName}Command.AddCommand({childName}Command);";
        sourceWriter.WriteLine(relationshipSource);
    }

    public void Visit(CommandOptionRelationship commandOptionRelationship)
    {
        var parentName = commandOptionRelationship.Parent.Name;
        var childName = commandOptionRelationship.Child.Name;
        var relationshipSource = $"{parentName}Command.AddOption({childName}Option);";
        sourceWriter.WriteLine(relationshipSource);
    }

    public void Visit(CommandArgumentRelationship commandArgumentRelationship)
    {
        var parentName = commandArgumentRelationship.Parent.Name;
        var childName = commandArgumentRelationship.Child.Name;
        var relationshipSource = $"{parentName}Command.AddArgument({childName}Argument);";
        sourceWriter.WriteLine(relationshipSource);
    }
}