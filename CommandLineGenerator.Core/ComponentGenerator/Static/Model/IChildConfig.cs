namespace CommandLineGenerator.ComponentGenerator.Static.Model;

public interface IChildConfig<T> where T:ICommandLineConfig
{
    public ICommandLineConfigRelationship GetParentRelationship(T parent);
}