using CommandLineGenerator.ComponentGenerator.Static.Model;
using CommandLineGenerator.SourceWriter;

namespace CommandLineGenerator.ComponentGenerator.Static;

public class ConfigRelationshipSourceVisitor(ISourceWriter sourceWriter): IConfigRelationshipVisitor
{
    public void Visit(CommandSubcommandRelationship commandSubcommandRelationship)
    {
        AddCommandChild("Command", commandSubcommandRelationship);
    }

    public void Visit(CommandOptionRelationship commandOptionRelationship)
    {
        AddCommandChild("Option", commandOptionRelationship);
    }

    public void Visit(CommandArgumentRelationship commandArgumentRelationship)
    {
        AddCommandChild("Argument", commandArgumentRelationship);
    }

    private void AddCommandChild(string childTypeName, ICommandLineConfigRelationship commandArgumentRelationship)
    {
        var parentName = commandArgumentRelationship.Parent.Name;
        var childName = commandArgumentRelationship.Child.Name;
        var relationshipSource = $"{parentName}Command.Add{childTypeName}({childName}{childTypeName});";
        sourceWriter.WriteLine(relationshipSource);
    }
}