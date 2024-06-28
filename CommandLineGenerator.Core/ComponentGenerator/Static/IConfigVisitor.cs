using CommandLineGenerator.ComponentGenerator.Static.Model;

namespace CommandLineGenerator.ComponentGenerator.Static;

public interface IConfigVisitor
{
    public void Visit(RootCommandConfigNode rootCommandConfigNode);
    public void Visit(CommandConfigNode commandConfigNode);
    public void Visit(OptionConfigNode optionConfigNode);
    public void Visit(ArgumentConfigNode argumentConfigNode);
}