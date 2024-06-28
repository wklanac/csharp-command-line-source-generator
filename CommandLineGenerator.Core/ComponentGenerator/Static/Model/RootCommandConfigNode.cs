using System.Collections;
using System.Collections.Immutable;

namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public record RootCommandConfigNode(
    string Description, List<ArgumentConfigNode> Arguments,
    List<OptionConfigNode> Options, List<CommandConfigNode> SubCommands) : 
    CommandConfigNode("root", Description, Arguments, Options, SubCommands), ICommandLineConfig
{
    public new void Accept(IConfigVisitor configVisitor)
    {
        configVisitor.Visit(this);
    }
}